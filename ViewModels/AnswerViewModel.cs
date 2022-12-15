using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.Models;
using Windows.UI.Xaml.Media;

namespace TriviaApp.ViewModels
{
    public class AnswerViewModel : INotifyPropertyChanged
    {
        private Answer answer;
        public event PropertyChangedEventHandler PropertyChanged;
        public AnswerViewModel()
        {
            answer = new Answer();
        }
        public string Text {
            get { return answer.Text; }

            set
            {
                answer.Text = value;
                OnPropertyChanged("Text");
            }
        }

        public bool IsCorrect
        {
            get { return answer.IsCorrect; }

            set
            {
                answer.IsCorrect = value;
                OnPropertyChanged("IsCorrect");
            }
        }

        public SolidColorBrush BgColor
        {
            get { return answer.BgColor; }

            set
            {
                answer.BgColor = value;

                // Let UI know BgColor property was changed
                OnPropertyChanged("BgColor");
            }
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
