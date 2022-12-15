using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using Windows.UI;
using TriviaApp.Models;
using Newtonsoft.Json;

namespace TriviaApp
{
    public class Question
    {
        private List<Answer> answers;

        [JsonProperty(PropertyName ="category")]
        public string Category { get; set; }


        [JsonProperty(PropertyName ="difficulty")]
        public string Difficulty { get; set; }


        [JsonProperty(PropertyName ="type")]
        public string Type { get; set; }


        [JsonProperty(PropertyName ="encoding")]
        public string Encoding { get; set; }


        [JsonProperty(PropertyName ="question")]
        public string Text { get; set; }


        [JsonProperty(PropertyName ="correct_answer")]
        public string CorrectAnswer { get; set; }


        [JsonProperty(PropertyName ="incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }

        public List<Answer> Answers
        {
            get
            {
                if (answers == null)
                {
                    answers = new List<Answer>();
                    foreach (string incAns in IncorrectAnswers)
                    {
                        answers.Add(new Answer
                        {
                            Text = incAns,
                            IsCorrect = false,
                            BgColor = new SolidColorBrush(Colors.Transparent)
                        });
                    }

                    answers.Insert(new Random().Next(answers.Count),
                        new Answer
                            {
                                Text = CorrectAnswer,
                                IsCorrect = true,
                                BgColor = new SolidColorBrush(Colors.Transparent)
                            }
                    );
                }
                return answers;
            }
        }
    }
}
