using ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Entities
{
    public class QuizItem : IIdentity<int>
    {
        public int Id { get; set; }
        public string Question { get; }
        public List<string> IncorrectAnswers { get; }    // problem!
        public string CorrectAnswer { get; }

        public QuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer)
        {
            Id = id;
            Question = question;
            IncorrectAnswers = incorrectAnswers;
            CorrectAnswer = correctAnswer;
        }
    }
}
