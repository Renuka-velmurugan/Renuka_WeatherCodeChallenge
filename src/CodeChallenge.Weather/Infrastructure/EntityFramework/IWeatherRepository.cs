namespace CodeChallenge.Weather.Infrastructure.EntityFramework
{
    using CodeChallenge.Weather.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWeatherRepository
    {
       //  Weather? FindById(Guid id);

       public string AddWeatherinInMemory(SensorsWeather sensors);
       public SensorsWeather GetWeatherfromInmemory(string city);
       public IEnumerable<SensorsWeather> GetAllWeather();



     //   void Remove(Weather domain);

        //  Task SaveAsync();
    }
}
