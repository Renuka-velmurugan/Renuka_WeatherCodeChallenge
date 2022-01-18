namespace CodeChallenge.Weather.Test.Infrastructure.OpenWeatherMap
{
    using CodeChallenge.Weather.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class OpenWeatherMapClientTests
    {
        private readonly IServiceProvider _serviceProvider;
        public readonly IWeatherClient _client;

        public OpenWeatherMapClientTests()
        {
            _serviceProvider = ConfigureServiceProvider().BuildServiceProvider();
            _client = _serviceProvider.GetService<IWeatherClient>()!;
        }

        [Fact]
        public async Task Should_GetWeatherMap_When_CityExists()
        {
            // Arrange
            var city = "Barcelona";

            // Act
            var actual = await _client.GetWeatherAsync(city);

            // Assert
            Assert.NotNull(actual);
        }

        private static IServiceCollection ConfigureServiceProvider()
        {
            var services = new ServiceCollection();

            // TODO Create a client that implement IWeatherClient and should be injected like that
            //services.AddHttpClient<IWeatherClient, OpenWeatherMapClient>(client =>
            //{
            //    client.BaseAddress = new Uri("https://api.openweathermap.org");
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //});

            return services;
        }
    }
}
