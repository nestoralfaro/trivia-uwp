using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp
{
    public class Question
    {
        public string category { get; set; }
        public string difficulty { get; set; }
        public string type { get; set; }
        public string encoding { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public List<string> incorrect_answers { get; set; }

        public List<string> answers {
            get {
                // Insert the correct answer at a random spot in the incorrect_answers list
                incorrect_answers.Insert(new Random().Next(incorrect_answers.Count), correct_answer);
                return incorrect_answers;
            }
        }

        private int RandomIndex(int lowerBound, int upperBound)
        {
            Random n = new Random();
            return 0;
        }

    }
}
