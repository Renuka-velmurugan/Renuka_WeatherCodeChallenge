using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Weather.Infrastructure.Model
{
    public class WeatherResponse
    {

        public class Coord
        {
            [Required]
            public double Lon { get; set; }

            [Required]
            public double Lat { get; set; }
        }

        public class Weather
        {
            [Required]
            public int Id { get; set; }

            [Required]
            public string? Main { get; set; }

            [Required]
            public string? Description { get; set; }

            [Required]
            public string? Icon { get; set; }
        }

  
    
        public class Main
        {
            [Required]
            public double Temp { get; set; }
            [Required]
            public double Feels_like { get; set; }

            [Required]
            public double Temp_min { get; set; }

            [Required]
            public double Temp_max { get; set; }

            [Required]
            public int Pressure { get; set; }

            [Required]
            public int Humidity { get; set; }
        }


        public class Wind
     {

            [Required]
            public double Speed { get; set; }

            [Required]
            public int Deg { get; set; }
        }

        public class Clouds
        {
            [Required]
            public int All { get; set; }
        }


        public class Sys
        {
            // [JsonProperty("sunrise")]
            [Required]
            public int Type { get; set; }

            [Required]
            public int Id { get; set; }

            [Required]
            public string? Country { get; set; }
           
            [Required]
            public int Sunrise { get; set; }
            [Required]
            public int Sunset { get; set; }
        }
        public class Root
        {
         // [JsonProperty("cod")]

          
            public Coord Coord { get; set; }

   
            public List<Weather> Weather { get; set; }
    
            public string? Base { get; set; }
      
            public Main Main { get; set; }

       
            public int Visibility { get; set; }
         
            public Wind? Wind { get; set; }
     
            public Clouds Clouds { get; set; }
    
            public int Dt { get; set; }
     
            public Sys Sys { get; set; }
         
            public int Timezone { get; set; }

       
            public int Id { get; set; }
           
            public string? Name { get; set; }

            [Required]
            public int Cod { get; set; }
        }

    }
}