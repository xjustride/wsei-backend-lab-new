using ApplicationCore.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Web;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI.Controllers;
using Xunit;

namespace IntegrationTest;

public class QuizApiTest
{
    [Fact]
    public async void GetShouldReturnQuiz()
    {
        //Arange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        
        //Act
        var result = await client.GetFromJsonAsync<Quiz>("api/v1/user/quizzes/1");
        
        //Assert
        Assert.NotNull((result));
        Assert.Equal(2, result.Id);
    }
}