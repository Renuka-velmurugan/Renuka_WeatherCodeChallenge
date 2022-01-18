namespace CodeChallenge.Weather.Domain.Service
{
    using CodeChallenge.Weather.Infrastructure.EntityFramework;
    using CodeChallenge.Weather.Infrastructure.Model;
    using Newtonsoft.Json;
    using System;
    using static CodeChallenge.Weather.Infrastructure.Model.WeatherResponse;

    public class WeatherDetectorService
    {       
        public IWeatherRepository _weatherrepository;
       // private WeatherContext _context;
        public WeatherDetectorService(IWeatherRepository weatherRepository)
        {        
            _weatherrepository = weatherRepository;
        }

        public SensorsWeather FillWeatherResponse(string jsonWeather)
        {
            try
            {
                WeatherResponse.Root weatherInfo = JsonConvert.DeserializeObject<WeatherResponse.Root>(jsonWeather);  //null handled previous
                SensorsWeather weather = LoadWeather(weatherInfo);
                return weather;
            }
            catch(Exception)
            {
               // string errMessage = ex.Message.ToString() +  " Error during deserialization, loading weather";  //log
                throw;
            }
        }

        public  SensorsWeather LoadWeather(WeatherResponse.Root weatherInfo)
        {
            try
            {
             SensorsWeather weather = new();

           // weather.ID = weatherInfo.Id;
            weather.Country = weatherInfo.Sys.Country;
            weather.City = weatherInfo.Name;
            weather.Lat = Convert.ToString(weatherInfo.Coord.Lat);
            weather.Lon = Convert.ToString(weatherInfo.Coord.Lon);
            weather.Description = weatherInfo.Weather[0].Description;
            weather.Humidity = Convert.ToString(weatherInfo.Main.Humidity);
            weather.Temp = Convert.ToString(weatherInfo.Main.Temp);
            weather.TempFeelsLike = Convert.ToString(weatherInfo.Main.Feels_like);
            weather.TempMax = Convert.ToString(weatherInfo.Main.Temp_max);
            weather.TempMin = Convert.ToString(weatherInfo.Main.Temp_min);
            return weather;

            }
            catch (Exception)
            {
                // string errMessage = ex.Message.ToString() +  " Error during deserialization, loading weather";  //log
                throw;
            }
        }
        public SensorsWeather LoadWeatherfromInMemory(string city)     
        {
            try
            {
              //  string petitionInfo = _weatherRepository.AddWeather(weatherReport);
                return _weatherrepository.GetWeatherfromInmemory(city);             
            }
            catch
            {
                throw;
            }
        }

         public string GetWeatherReportfromInMemory(string city)
        {   
            
         try
            {
                //Operation to get weather report from the InMemoryDB
                SensorsWeather weather = LoadWeatherfromInMemory(city);
                String report = String.Empty;
                double cityTemperature = Convert.ToDouble(weather.Temp);

                if (weather != null)
                {
                    //BL based on Temperature

                    if (cityTemperature < 0)
                        report = "Bad Weather - Freezing temperature in " + weather.City + " - " + weather.Description;
                    else if (cityTemperature < 10)
                        report = "Bad Weather - Very cold temperature in " + weather.City + " - " + weather.Description;
                    else if (cityTemperature < 20)
                        report = "Cold weather in " + weather.City + " - " + weather.Description;
                    else if (cityTemperature < 30)
                        report = "Good Weather - Normal temperature in " + weather.City + " - " + weather.Description;
                    else if (cityTemperature < 40)
                        report = "Its Hot, bad weather in " + weather.City + " - " + weather.Description;
                    else
                        report = "Its very hot bad weather " + weather.City + " - " + weather.Description;

                    //if (weather.Description == "rain" || weather.Description == "snow" || weather.Description == "fog" || weather.Description == "windy" || weather.Description == "light snow")
                    //{
                    //    report = weather.City + ": Bad weather due to " + weather.Description + " ";
                    //}
                }

                return report;
            }
            catch 
            {
                throw; 
            }
        }
      
    }
}
