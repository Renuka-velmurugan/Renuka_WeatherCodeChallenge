namespace CodeChallenge.Weather.Infrastructure
{
    using CodeChallenge.Weather.Infrastructure.Model;
    using System.Threading.Tasks;

    public interface IWeatherClient
    {
        Task<string> GetWeatherAsync(string city);
    }
}