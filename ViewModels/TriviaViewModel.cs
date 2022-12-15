using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using TriviaApp.Models;

namespace TriviaApp.ViewModels
{
    public class TriviaViewModel : INotifyPropertyChanged
    {
        private Trivia trivia;
        private readonly HttpClient client;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        public TriviaViewModel()
        {
            trivia = new Trivia();
            client = new HttpClient();
            Questions = new ObservableCollection<QuestionViewModel>();

            //Create ViewModels for each Question
            foreach (Question question in trivia.Questions)
            {
                QuestionViewModel newQuestion = new QuestionViewModel
                {
                    Text = question.Text,
                    Answers = GetAnswerViewModels(question.Answers),
                    Category = question.Category,
                    Difficulty = question.Difficulty,
                };
                newQuestion.PropertyChanged += OnPropertyChanged;
                Questions.Add(newQuestion);
            }
        }

        public async Task FetchQuestions(int count)
        {
            var response = await client.GetAsync($"https://opentdb.com/api.php?amount={count}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            if (data != null)
            {
                var dataObj = JObject.Parse(data)["results"];
                foreach (var q in dataObj)
                {
                    Question question = JsonConvert.DeserializeObject<Question>(q.ToString());
                    QuestionViewModel newQuestion = new QuestionViewModel
                    {
                        Text = question.Text,
                        Answers = GetAnswerViewModels(question.Answers),
                        Category = question.Category,
                        Difficulty = question.Difficulty,
                    }; 
                    newQuestion.PropertyChanged += OnPropertyChanged;
                    Questions.Add(newQuestion);
                }
            }
        }

        // Create ViewModels for each Answer
        private List<AnswerViewModel> GetAnswerViewModels(List<Answer> answers)
        {
            List<AnswerViewModel> ansvms = new List<AnswerViewModel>();
            foreach (Answer ans in answers)
            {
                ansvms.Add(new AnswerViewModel
                {
                    Text = ans.Text,
                    BgColor = ans.BgColor,
                    IsCorrect = ans.IsCorrect,
                });
            }
            return ansvms;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Theater new or MovieViewModel changed, so let UI know
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(sender, e);
            }
        }
    }
}
