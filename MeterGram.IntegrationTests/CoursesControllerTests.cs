using MeterGram.IntegrationTests.Factories;
using MeterGram.WebApi.Contracts.Responses.Course;
using System.Net.Http.Json;

namespace MeterGram.IntegrationTests
{
    public class CoursesControllerTests : IClassFixture<IntegrationTestWebFactory>
    {
        private readonly IntegrationTestWebFactory _factory;

        public CoursesControllerTests(IntegrationTestWebFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_CarList_ReturnsActiveCars()
        {// Arrange
            var client = _factory.CreateClient();

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>("/api/v1/courses");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }
        [Fact]
        public async Task Get_CarList_ReturnsAllCars()
        {// Arrange
            var client = _factory.CreateClient();

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>("/api/v1/courses?onlyActive=false");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }
    }
}