using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.CodeDom.Compiler;
using TriviaApp.ViewModels;
using TriviaApp.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TriviaApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Store the question, and the answer selected
        private Dictionary<string, string> attempts = new Dictionary<string, string>();
        public TriviaViewModel Trivia { get; set; }
        public QuestionViewModel Question { get; set; }
        public AnswerViewModel Answer { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Trivia = new TriviaViewModel();
            Question = new QuestionViewModel();
            Answer = new AnswerViewModel();
        }

        private async void GetQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ManyQuestionsTextBox.Text;
            if (!Int32.TryParse(input, out int manyQuestions) || !Regex.IsMatch(input, @"^[0-9]+$"))
            {
                // If it was not able to parse the string or it was a negative number
                // Show the error message
                WrongNumberErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                Trivia.Questions.Clear();
                WrongNumberErrorMessage.Visibility = Visibility.Collapsed;
                await Trivia.FetchQuestions(manyQuestions);
                CheckAnswersButton.Visibility = Visibility.Visible;
            }
        }

        private void CheckAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (QuestionViewModel question in Trivia.Questions)
            {
                foreach (AnswerViewModel answer in question.Answers)
                {
                    answer.BgColor = answer.IsCorrect ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.LightPink); 
                }
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selection = sender as RadioButton;
            string question = selection.GroupName;
            string answer = selection.Content.ToString();
            if (question != null && answer != null)
            {
                if (!attempts.ContainsKey(question))
                {
                    attempts.Add(question, answer);
                }
            }
        }
    }
}
