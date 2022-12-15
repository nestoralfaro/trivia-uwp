using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace TriviaApp.Models
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public SolidColorBrush BgColor { get; set; }
    }
}
