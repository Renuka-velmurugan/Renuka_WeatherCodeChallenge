namespace CodeChallenge.Weather.Infrastructure.EntityFramework
{
    using CodeChallenge.Weather.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WeatherContext : DbContext
    {     
        public  DbSet<SensorsWeather> Weather { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)  
        {

         //using In memory DB
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Do nothing because of X and Y.            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
