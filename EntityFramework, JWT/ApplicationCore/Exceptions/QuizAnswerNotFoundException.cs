using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class QuizAnswerNotFoundException : Exception
    {
        public int UserId { get; }
        public int QuizId { get; }
        public int QuizItemId { get; }

        public QuizAnswerNotFoundException(int quizId, int quizItemId, int userId)
            : base($"Answer for quiz item {quizItemId} in quiz {quizId} by user {userId} was not found.")
        {
            QuizId = quizId;
            QuizItemId = quizItemId;
            UserId = userId;
        }
    }
}
