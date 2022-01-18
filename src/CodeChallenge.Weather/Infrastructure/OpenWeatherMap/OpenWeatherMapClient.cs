using CodeChallenge.Weather.Domain.Service;
using CodeChallenge.Weather.Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http;

namespace CodeChallenge.Weather.Infrastructure.OpenWeatherMap
{  
    public class OpenWeatherMapClient : IWeatherClient
    {
        private readonly IConfiguration _configuration;    
        public OpenWeatherMapClient(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
                 
        public async Task<string> GetWeatherAsync(string city)
        {
            try
            {
               string appId = _configuration.GetSection("WeatherApi").GetSection("ApiAppId").Value; //reading from appsetting.dev.json  file
               string apiUri = _configuration.GetSection("WeatherApi").GetSection("ApiUri").Value;

               using var client = new HttpClient(); 
               client.BaseAddress = new Uri(apiUri); //conecting weather API
               var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&APPID={appId}");
               response.EnsureSuccessStatusCode();   // getting Json weather report              
               var jsonWeatherReport = await response.Content.ReadAsStringAsync(); //Json content
               string weatherJson = jsonWeatherReport.ToString();

               if(string.IsNullOrEmpty(weatherJson ))
               {
                    weatherJson = "invalidcity";
                    //  InvalidCityNameException c = new InvalidCityNameException();                  
                    //  throw new HttpResponseException(HttpStatusCode.NotFound);   //this  asp.net webapi.core pacakge not supported in this project
                    //throw new FileNotFoundException("Invalid City name. City Name not exists in WebAPI");

                }

                return weatherJson;
            }           
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                throw;
            }
            catch (FileNotFoundException ex)
            {
                throw ex;                 
               // throw new HttpResponseException// log
             
            }
            catch
            {
                throw;

            }

        }
    }
}



//backup
// WeatherDetectorService wService = new WeatherDetectorService(result);
//  WeatherResponse.Root weatherInfo = JsonConvert.DeserializeObject<WeatherResponse.Root>(jsonWeatherReport);

//  var reports = JsonConvert.DeserializeObject<IEnumerable<WeatherResponse>>(jsonWeatherReport);

// var w = JsonConvert.DeserializeObject<IEnumerable<WeatherResponse.Root>>(jsonWeatherReport);

// var w1 = JsonConvert.DeserializeObject<IEnumerable<WeatherResponse.Clouds>>(jsonWeatherReport);

// WeatherResponse? weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonWeatherReport);