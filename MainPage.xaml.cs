using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using TriviaApp.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TriviaApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Store the question, and the answer selected
        private Dictionary<string, TaggedAnswer> attempts;
        public TriviaViewModel Trivia { get; set; }
        public QuestionViewModel Question { get; set; }
        public AnswerViewModel Answer { get; set; }
        public ScoreViewModel Score { get; set; }
        private bool readyForChecking = false;

        public MainPage()
        {
            InitializeComponent();
            Trivia = new TriviaViewModel();
            Question = new QuestionViewModel();
            Answer = new AnswerViewModel();
            Score = new ScoreViewModel{ CorrectOnesCount=0, WrongOnesCount=0 };
            attempts = new Dictionary<string, TaggedAnswer>();
        }

        private async void GetQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (readyForChecking)
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

                    try
                    {
                        LoadingRing.IsActive = true;
                        await Trivia.FetchQuestions(manyQuestions);
                    }

                    catch (Exception err)
                    {
                        Console.WriteLine($"{err} Exception caught.");
                    }

                    finally
                    {
                        LoadingRing.IsActive = false;
                    }

                    CheckAnswersButton.Visibility = Visibility.Visible;
                }

                readyForChecking = true;
            }
        }

        private void CheckAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            if (readyForChecking)
            {
                foreach (QuestionViewModel question in Trivia.Questions)
                {
                    foreach (AnswerViewModel answer in question.Answers)
                    {
                        answer.BgColor = answer.IsCorrect ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.LightPink); 
                    }
                }

                foreach (KeyValuePair<string, TaggedAnswer> attempt in attempts)
                {
                    if (attempt.Value.IsCorrect)
                    {
                        ++Score.CorrectOnesCount;
                    }

                    if (!attempt.Value.IsCorrect)
                    {
                        ++Score.WrongOnesCount;
                    }
                }

                readyForChecking = false;
            }

            ScoreBox.Visibility = Visibility.Visible;
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selection = sender as RadioButton;
            string question = selection.GroupName;
            string answer = selection.Content.ToString();
            bool isCorrect = (bool)selection.Tag;
            if (question != null && answer != null)
            {
                if (!attempts.ContainsKey(question))
                {
                    attempts.Add(question, new TaggedAnswer { Answered=answer, IsCorrect=isCorrect });
                }
                else
                {
                    attempts[question] = new TaggedAnswer { Answered = answer, IsCorrect = isCorrect };
                }
            }
        }
    }

    public class TaggedAnswer
    {
        public string Answered { get; set; }
        public bool IsCorrect { get; set; }
    }
}
