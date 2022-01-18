using CodeChallenge.Weather.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Weather.Infrastructure.EntityFramework
{
    public class EfWeatherRepository: IWeatherRepository
    {

        private WeatherContext _weatherContext;
       // private SensorsWeather _sensors;
       //   public static List<SensorsWeather> cityWeatherList = new();
        public EfWeatherRepository(WeatherContext weatherContext)
        {
            _weatherContext = weatherContext;
           //_sensors = sensors;

        }
      
        public string  AddWeatherinInMemory(SensorsWeather sWeather)
        {
            try
            {
                string city = sWeather.City;
                string petitionInfo = string.Empty;
         
                if (sWeather != null)
                {
                    if (!(_weatherContext.Weather.Any(e => e.City == city)))
                    {
                        _weatherContext.Weather.Add(sWeather);
                        _weatherContext.SaveChanges();

                        petitionInfo = "The Petition for the city: " + city + " addedd successfully to the inmemory weather DB.";
                        return petitionInfo;
                    }
                    else
                    {
                    
                        petitionInfo = "duplicate";
                        return petitionInfo;
                    }

                  
                }
                else
                {
                   // return "nodata";
                   throw new ArgumentNullException("Invalid City, need correct input");  //need to create error class to give the 

                }
            }
            catch
            {
                throw;
            }

        }
        public IEnumerable<SensorsWeather> GetAllWeather()
        {
            try
            {
                return _weatherContext.Weather;
            }
            catch
            {

                throw;
            }
        }

        public SensorsWeather GetWeatherfromInmemory(string uCity)
        {
            // searching in memory for the given city

            try
            {
            
                var order = _weatherContext.Weather.SingleOrDefault(e => e.City == uCity); // to check null

                if (order != null)
                {
                    return _weatherContext.Weather.SingleOrDefault(e => e.City == uCity);  //null checked previous
                }
                else
                {

                    throw new ArgumentNullException( uCity + " not exists in in memory DB");

                }

            }
            catch (Exception)
            {
                throw;

            }




        }
    }
}
