using MeterGram.IntegrationTests.Factories;
using MeterGram.WebApi.Contracts.Responses.Course;
using System.Net.Http.Json;
using System.Text;

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
        public async Task Get_CourseList_ReturnsActiveCourses()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>("/api/v1/courses");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task Get_CourseList_ReturnsAllCourses()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act 
            var result = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>("/api/v1/courses?onlyActive=false");

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task Post_SynchronizeCourses_UpdatesData()
        {
            // Arrange
            var client = _factory.CreateClient();

            var result = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>("/api/v1/courses?onlyActive=false");
            result.Should().NotBeNull();
            result.First(x => x.Id == 4).IsActive.Should().Be(false);
            result.First(x => x.Id == 5).IsActive.Should().Be(true);

            // Act 
            await client.PostAsync("/api/v1/courses/synchronize", new StringContent(string.Empty, Encoding.UTF8, "application/json"));

            // Assert
            result = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>("/api/v1/courses?onlyActive=false");
            result.Should().NotBeNull();
            result.First(x => x.Id == 4).IsActive.Should().Be(true);
            result.First(x => x.Id == 5).IsActive.Should().Be(false);
        }
    }
}