using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Weather.Domain
{
    public  class SensorsWeather
    {
        
     //   [Required]
    //    public int ID { get; set; }
        [Key]   
        public string? City { get; set; }
    
        public string? Country { get; set; }
  
        public string? Lat { get; set; }

        public string? Lon { get; set; }
    
        public string? Description { get; set; }
 
        public string? Humidity { get; set; }
     
        public string? TempFeelsLike { get; set; }
     
        public string? Temp { get; set; }
     
        public string? TempMax { get; set; }
    
        public string? TempMin { get; set; }

    }
}
