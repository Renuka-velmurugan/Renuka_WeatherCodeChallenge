using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Weather.Api.Model
{
    public class WeatherCity
    {
        [Required]     // preventing null or empty
        public string City { get; set; }

       // [Required]
      //  public int? ID { get; set; }  //weather API only needed City Name and App Id, Id not required.
    }
}
