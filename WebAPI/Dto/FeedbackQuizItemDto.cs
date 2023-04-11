namespace WebAPI.Controllers;

public class FeedbackQuizItemDto
{
    public int QuizItemId { get; init; }
    public string Answer { get; init; }
    public bool IsCorrect { get; init; }
}