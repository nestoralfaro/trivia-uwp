using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp.Models
{
    public class Trivia
    {
        public List<Question> Questions { get; set; }
        public Trivia()
        {
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Welcome to the Trivia App!",
                    Category = "By Nestor Alfaro",
                    Difficulty = "Easy Peasy",
                    CorrectAnswer = "Enter many questions!",
                    IncorrectAnswers = new List<string>() { "To Start", "And test", "Your intelecto :O" },
                }
            };
        }
    }
}
