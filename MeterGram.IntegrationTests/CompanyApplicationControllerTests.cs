using MeterGram.IntegrationTests.Factories;
using MeterGram.IntegrationTests.TestData;
using MeterGram.WebApi.Contracts.Requests.CompanyApplications;
using MeterGram.WebApi.Contracts.Responses;
using MeterGram.WebApi.Contracts.Responses.CompanyApplications;
using MeterGram.WebApi.Contracts.Responses.Course;
using System.Net.Http.Json;

namespace MeterGram.IntegrationTests;

public class CompanyApplicationControllerTests : IClassFixture<IntegrationTestWebFactory>
{
    private readonly IntegrationTestWebFactory _factory;

    public CompanyApplicationControllerTests(IntegrationTestWebFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_ApplicationList_ReturnsAllApplications()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act 
        // Adding name filter to stop false negatives from other tests
        var result = await client.GetFromJsonAsync<PagedResponse<CompanyApplicationResponse>>("/api/v1/company-applications?Name=MeterGram");

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
    }

    [Fact]
    public async Task Get_ApplicationListWithFilterThatDoesntMatch_ReturnsAllApplications()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act 
        // Adding name filter to stop false negatives from other tests
        var result = await client.GetFromJsonAsync<PagedResponse<CompanyApplicationResponse>>("/api/v1/company-applications?Name=Endava");

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(0);
    }

    [Fact]
    public async Task Get_ApplicationListForParticipant_ReturnsAllApplications()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act 
        // Adding name filter to stop false negatives from other tests
        var result = await client.GetFromJsonAsync<PagedResponse<CompanyApplicationResponse>>("/api/v1/company-applications/participant?Name=Aleksandar");

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
    }

    [Fact]
    public async Task Get_ApplicationListForParticipantWithFilterThatDoesntMatch_ReturnsAllApplications()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act 
        // Adding name filter to stop false negatives from other tests
        var result = await client.GetFromJsonAsync<PagedResponse<CompanyApplicationResponse>>("/api/v1/company-applications/participant?Name=Petar");

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(0);
    }

    [Theory]
    [ClassData(typeof(CreateNewApplicationTestData))]
    public async Task Post_CreateNewApplication_CreatesRecordInDatabase(CompanyApplicationCreateRequest request)
    {
        // Arrange
        var client = _factory.CreateClient();

        var result = await client.GetFromJsonAsync<PagedResponse<CompanyApplicationResponse>>($"/api/v1/company-applications?Name={request.Name}");

        result.Should().NotBeNull();
        result.Data.Should().HaveCount(0);


        await client.PostAsJsonAsync($"/api/v1/company-applications", request);

        // Assert
        result = await client.GetFromJsonAsync<PagedResponse<CompanyApplicationResponse>>($"/api/v1/company-applications?Name={request.Name}");

        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data[0].Name.Should().Be(request.Name);
        result.Data[0].Participants.Should().HaveCount(1);
        result.Data[0].Participants[0].Name.Should().Be(request.Participants[0].Name);

    }
}