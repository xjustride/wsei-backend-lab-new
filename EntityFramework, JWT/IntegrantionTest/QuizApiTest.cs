using ApplicationCore.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Web;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI.Controllers;
using Xunit;
using System.Net;

namespace IntegrationTest;

public class QuizApiTest
{
    [Fact]
    public async void GetShouldReturnTwoQuizzes()
    {
        //Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        //Act
        var result = await client.GetFromJsonAsync<List<QuizDto>>("/api/v1/quizzes");

        //Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async void GetShouldReturnOkStatus()
    {
        //Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        //Act
        var result = await client.GetAsync("/api/v1/quizzes");

        //Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
    }


}