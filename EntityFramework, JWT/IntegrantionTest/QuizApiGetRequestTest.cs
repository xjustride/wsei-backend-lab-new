﻿using Infrastructure.Entites;
using Infrastructure;
using System.Net;
using WebAPI.Controllers;
using Xunit;

namespace IntegrantionTest
{
    public class QuizApiGetRequestTest : IClassFixture<QuizAppTestFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly QuizAppTestFactory<Program> _app;
        private readonly QuizDbContext _context;
        public QuizApiGetRequestTest(QuizAppTestFactory<Program> app)
        {
            _app = app;
            _client = app.CreateClient();
            using (var scope = app.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetService<QuizDbContext>();
                var items = new HashSet<QuizItemEntity>
            {
                new()
                {
                    Id = 1, CorrectAnswer = "7", Question = "2 + 5", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 11, Answer = "5"},
                            new() {Id = 12, Answer = "6"},
                            new() {Id = 13, Answer = "8"},
                        }
                },
            };
                if (_context.Quizzes.Count() == 0)
                {
                    _context.Quizzes.Add(
                        new QuizEntity
                        {
                            Id = 1,
                            Items = items,
                            Title = "Matematyka"
                        }
                    );
                    _context.SaveChanges();
                }
            }
        }
        [Fact]
        public async void GetShouldReturnTwoQuizzes()
        {
            //Arrange

            //Act
            var result = await _client.GetFromJsonAsync<List<QuizDto>>("/api/v1/quizzes");

            //Assert
            if (result != null)
            {
                Assert.Single(result);
                Assert.Equal("Matematyka", result[0].Title);
            }
        }

        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange

            //Act
            var result = await _client.GetAsync("/api/v1/quizzes");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }
    }
}
