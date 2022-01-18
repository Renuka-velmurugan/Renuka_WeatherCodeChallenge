namespace CodeChallenge.Weather.Api.Controllers
{
    using CodeChallenge.Weather.Api.Model;
    using CodeChallenge.Weather.Domain;
    using CodeChallenge.Weather.Domain.Service;
    using CodeChallenge.Weather.Infrastructure;
    using CodeChallenge.Weather.Infrastructure.EntityFramework;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Weather API v1
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
       //  private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherClient _weatherclient;
        private readonly IWeatherRepository _weatherRepository;

        /// <summary>
        /// Constructor of weather controller
        /// </summary>
        ///  <param name="weatherclient"></param>
        /// <param name="weatherRepository"></param>
        public WeatherController(IWeatherClient weatherclient, IWeatherRepository weatherRepository)
        {
            //_logger = logger;
            _weatherclient = weatherclient;
            _weatherRepository = weatherRepository;
        }

        /// <summary>
        /// Get the information from the repository about the city, when is bad weather also includes the sensor(s)
        /// to explain the cause of the bad weather.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Barcelona
        ///
        /// </remarks>
        /// <param name="city"></param>
        /// <returns>Returns a model that includes all of this information</returns>
        /// <response code="200">Returns the results</response>
        /// <response code="400">If the requests is invalid</response>
        [HttpGet("{city}", Name = "GetCityWeather")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string city)
        {
            try
            {
                //Get the weather from IWeatherRepository(Inmemory) and  Returns good weather or bad weather

                Regex onlyAlphabets = new("^[a-zA-Z ]+$");
                string tCity = city.Trim();

                if (onlyAlphabets.IsMatch(tCity.Trim()))
                {                 
                    WeatherDetectorService wDetectorService = new(_weatherRepository); // To get stored datas from in memeory
                    string uCity = tCity.Substring(0, 1).ToUpper() + tCity.Substring(1).ToLower(); // Inmemory have city in this format case
                    var weatherReport = wDetectorService.GetWeatherReportfromInMemory(uCity);  //get given city details,to performed BL 

                    if (weatherReport == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "The " + tCity + " not exist in the Inmemory collection, please add the city into Post method petition, Record not found, check Input");
                    }
                    return StatusCode(StatusCodes.Status200OK, weatherReport); // return weather report
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Invalid City Name, please provide Correct Name");
                    // return StatusCode(StatusCodes.Status400BadRequest, "Inputs not provided / incorrect inputs, please provide city details"); //null input
                }
            }
            catch (ArgumentNullException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Fetching from InMemory Collection, so please Post the city in petition first");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }

        }

        /// <summary>
        /// Process a new petition to get sensros from OpenWeatherMap API, detects the weather and stores in a repository
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "city": "Barcelona"
        ///     }
        ///
        /// </remarks>
        /// 
        /// <param name="weatherCity"></param>
        /// <returns>The status of the petition</returns>
        /// <response code="201">Returns when is created succesfully</response>
        /// <response code="400">Returns for a bad request</response>
        /// <response code="406">Returns when other internal resasons</response>
        /// <response code="409">Returns if id already exists</response>
        [HttpPost(Name = "StoreCityWeather")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public IActionResult Post([FromBody] WeatherCity weatherCity)
        {
            try
            {
                Regex onlyAlphabets = new("^[a-zA-Z ]+$");
                string tCity = weatherCity.City.Trim();

                if (onlyAlphabets.IsMatch(tCity))                  
                {              
                    WeatherDetectorService wDetectorService = new(_weatherRepository);  // Call WeatherDetectorService, to store the results together with the sensors 
                    var apiWeatherResponse = _weatherclient.GetWeatherAsync(tCity);   // Called the OpenWeatherMap API with the city from body
                    if(apiWeatherResponse.ToString() == "invalidcity")
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable, "Invalid City Name, please provide Correct Name");
                    }

                    SensorsWeather weatherReport = wDetectorService.FillWeatherResponse(apiWeatherResponse.Result);               
                    string petitionStatus = _weatherRepository.AddWeatherinInMemory(weatherReport); // To Store SensorsWeather datas into the repository IWeatherRepository and get the petition status                  

                    ResponseJson responseJson = new()   // creating  Json Response
                    {
                    WeatherJson = apiWeatherResponse.Result
                     };  
                 
                    if (petitionStatus == "duplicate")
                    {
                        petitionStatus = "The Petition for the city: " + tCity + " not added, record exists already, duplicate request";
                    //  return StatusCode(StatusCodes.Status409Conflict, petitionInfo);  // will get weather json report, but petition failed // if needed will throw exception
                    }

                    responseJson.PetitionStatus = petitionStatus;
                    return StatusCode(StatusCodes.Status200OK, responseJson);  //  Json Response with weather and Petition Status
                }
                else
                {
                   return StatusCode(StatusCodes.Status406NotAcceptable, "Invalid City Name, please provide Correct Name");
                }
            }      
            catch (WebException ex)
            {
                if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message.ToString() + "Invalid City name " + weatherCity.City + "please provide valid city name");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Web exception " + ex.Message.ToString());
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message.ToString() + " : Invalid City Name, City not exist in weather API");
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message.ToString());  
            }
        }


        /// <summary>
        ///To check List of items added in InMemory Weather DB
        /// </summary>
        [HttpGet]
        public IEnumerable<SensorsWeather> GetAllWeather()
        {          
                return _weatherRepository.GetAllWeather();

        }
    }
}
