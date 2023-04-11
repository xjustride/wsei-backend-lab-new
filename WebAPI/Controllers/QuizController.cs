using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("/api/v1/quizzes")]
public class QuizController: ControllerBase
{
    private readonly IQuizUserService _service;

    public QuizController(IQuizUserService service)
    {
        _service = service;
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDto> FindById(int id)
    {
        var result = QuizDto.of(_service.FindQuizById(id));
        return result is null ?  NotFound() : Ok(result);
    }

    [HttpGet]
    public IEnumerable<QuizDto> FindAll()
    {
        return _service.FindAllQuizzes().Select(QuizDto.of).AsEnumerable();
    }

    [HttpPost]
    [Route("{quizId}/items/{itemId}/answers")]
    public ActionResult SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
    {
        try
        {
            var answer = _service.SaveUserAnswerForQuiz(quizId, dto.UserId, itemId, dto.UserAnswer);
            return Created("", answer);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet, Produces("application/json")]
    [Route("{quizId}/feedbacks")]
    public FeedbackQuizDto GetFeedback(int quizId)
    {
        int userId = 1;
        var answers = _service.GetUserAnswersForQuiz(quizId, userId);
        //TODO: wykonaj mapowanie listy odpowiedzi na obiekt FeedbackQuizDto 
    }
}