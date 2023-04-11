namespace WebAPI.Controllers;

public class FeedbackQuizDto
{
    public int QuizId { get; init; }

   public int ValidAnswersCounter { get; set; }
   
   public int TotalQuizItemsCounter { get; set; }
   
   public List<FeedbackQuizItemDto> QuizItemsAnswers{get; init; }
}