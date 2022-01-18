namespace CodeChallenge.Weather.Test.Application
{
    using CodeChallenge.Weather.Domain;
    using CodeChallenge.Weather.Domain.Service;
    using Xunit;

    public class WeatherDetectorServiceTest
    {
        private readonly WeatherDetectorService _service;

        public WeatherDetectorServiceTest()
        {
            _service = new WeatherDetectorService();
        }

        [Fact]
        public void Should_ReturnGoodWeather_When_HasGoodTemperature()
        {
            // Arrange
            var sensor = new SensorsWeather()
            {
                TemperatureInCelsius = 20,
            };

            // Act
            var actual = _service.GetWeather(sensor);

            // Assert
            Assert.Equal("is having a good weather", actual);
        }

        [Fact]
        public void Should_ReturnBadWeather_When_IsRaining()
        {
            // Arrange
            var sensor = new SensorsWeather()
            {
                RainVolume = 10,
            };

            // Act
            var actual = _service.GetWeather(sensor);

            // Assert
            Assert.Equal("is having a bad weather because is raining", actual);
        }

        [Fact]
        public void Should_ReturnBadWeather_When_IsSnowing()
        {
            // Arrange
            var sensor = new SensorsWeather()
            {
                SnowVolume = 10,
            };

            // Act
            var actual = _service.GetWeather(sensor);

            // Assert
            Assert.Equal("is having a bad weather because is snowing", actual);
        }

        [Fact]
        public void Should_ReturnBadWeather_When_HardWindy()
        {
            // Arrange
            var sensor = new SensorsWeather()
            {
                WindSpeed = 80,
            };

            // Act
            var actual = _service.GetWeather(sensor);

            // Assert
            Assert.Equal("is having a bad weather because is windy", actual);
        }
    }
}
