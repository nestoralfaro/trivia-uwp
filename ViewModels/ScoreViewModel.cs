using System.ComponentModel;
using TriviaApp.Models;

namespace TriviaApp.ViewModels
{
    public class ScoreViewModel : INotifyPropertyChanged
    {
        private Score score;
        public event PropertyChangedEventHandler PropertyChanged;

        public ScoreViewModel()
        {
            score = new Score(); 
        }

        public int CorrectOnesCount
        {
            get { return score.CorrectOnesCount; }

            set
            {
                score.CorrectOnesCount = value;
                OnPropertyChanged("CorrectOnesCount");
            }
        }

        public int WrongOnesCount
        {
            get { return score.WrongOnesCount; }

            set
            {
                score.WrongOnesCount = value;
                OnPropertyChanged("WrongOnesCount");
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
