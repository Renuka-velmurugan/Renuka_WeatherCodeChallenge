namespace RepIntel.Ingestion.Tests.WebsiteMetrics.Infrastructure.EntityFramework
{
    using CodeChallenge.Weather.Domain;
    using CodeChallenge.Weather.Infrastructure.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Xunit;

    public class EfWeatherRepositoryTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWeatherRepository _repository;

        public EfWeatherRepositoryTests()
        {
            _serviceProvider = ConfigureServiceProvider().BuildServiceProvider();
            _repository = _serviceProvider.GetService<IWeatherRepository>()!;
        }

        [Fact]
        public void Should_SaveFindAndRemove_When_WeatherIsValid()
        {
            // Arrange
            var actual = new Weather();

            // Act
            _repository.Add(actual);
            _repository.SaveAsync();
            var expected = _repository.FindById(actual.Id)!;
            _repository.Remove(actual);
            _repository.SaveAsync();

            // Assert
            Assert.Equal(expected, actual);
        }

        private static IServiceCollection ConfigureServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<WeatherContext>(options => options.UseInMemoryDatabase(databaseName: "Weather"));

            // TODO: Implement a EntityFramework repository and should be injected like that
            //services.AddScoped<IWeatherRepository, EfWeatherRepository>();

            return services;
        }
    }
}
