using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace TriviaApp.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private Question question;
        private List<AnswerViewModel> answers;
        public event PropertyChangedEventHandler PropertyChanged;

        public QuestionViewModel()
        {
            question = new Question();
            answers = new List<AnswerViewModel>();
        }

        public string Text
        {
            set
            {
                question.Text = value;
                OnPropertyChanged("Text");
            }

            // The raw response may include HTML encoded characters 
            get { return WebUtility.HtmlDecode(question.Text); }
        }

        public List<AnswerViewModel> Answers
        {
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }

            get
            {
                return answers;
            }
        }

        public string Category
        {
            set
            {
                question.Category = value;
                OnPropertyChanged("Category");
            }

            get { return question.Category; }
        }

        public string Difficulty
        {
            set
            {
                question.Difficulty = value;
                OnPropertyChanged("Difficulty");
            }

            get { return question.Difficulty; }
        }
        private void OnPropertyChanged(string property)
        {
            // Notify any controls bound to the ViewModel that the property changed
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
