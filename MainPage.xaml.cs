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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TriviaApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly HttpClient client;
        private ObservableCollection<Question> questionList;

        // Store the question, and the answer selected
        private Dictionary<string, string> attempts = new Dictionary<string, string>();

        public MainPage()
        {
            InitializeComponent();
            client = new HttpClient();
            questionList = new ObservableCollection<Question> { new Question {
                    question = "Welcome to the Trivia App!",
                    category = "By Nestor Alfaro",
                    difficulty = "Easy Peasy",
                    correct_answer = "Enter many questions!",
                    incorrect_answers = new List<string>()
                        {
                            "To Start",
                            "And test",
                            "Your intelecto :O"
                        }
                }
            };
        }

        private async Task AddTriviaQuestions(int count)
        {
            var response = await client.GetAsync($"https://opentdb.com/api.php?amount={count}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            if (data != null)
            {
                var dataObj = JObject.Parse(data)["results"];
                Debug.WriteLine("the parsed obj");
                foreach (var q in dataObj)
                {
                    Question question = JsonConvert.DeserializeObject<Question>(q.ToString());
                    questionList.Add(question);
                }
            }
        }

        private async void GetQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ManyQuestionsTextBox.Text;
            int manyQuestions;
            if (!Int32.TryParse(input, out manyQuestions) || !Regex.IsMatch(input, @"^[0-9]+$"))
            {
                // If it was not able to parse the string or it was a negative number
                // Show the error message
                WrongNumberErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                questionList.Clear();
                WrongNumberErrorMessage.Visibility = Visibility.Collapsed;
                await AddTriviaQuestions(manyQuestions);
                CheckAnswersButton.Visibility = Visibility.Visible;
            }
        }

        private void CheckAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            //var radioButtonItems = (string[])DataContext;
            //var btn = (RadioButton)FindName("To Start");
            //btn.Background = new SolidColorBrush(Colors.Green);

            foreach (Question elem in questionList)
            {
                //string correctAnswerName = CreateValidIdentifier(elem.correct_answer);
                //var btn = QuestionListBox.FindName(correctAnswerName) as RadioButton;
                //btn.Background = new SolidColorBrush(Colors.Green);
                string correctAnswerName = CreateValidIdentifier(elem.correct_answer);
                var btn = QuestionListBox.FindName(correctAnswerName) as RadioButton;
                btn.Background = new SolidColorBrush(Colors.Green);
            }




            // this works
            //RadioButton b = FindName("idk") as RadioButton;
            //b.Background = new SolidColorBrush(Colors.Green);
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

        private string CreateValidIdentifier (string name)
        {
            string identifier = Regex.Replace(name, @"[^a-zA-Z0-9_]", "");

            if (char.IsDigit(identifier[0]))
            {
                identifier = "_" + identifier;
            }

            return identifier;
        }

        private void RadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            //RadioButton radioButton = sender as RadioButton;
            //string newName = CreateValidIdentifier(radioButton.Content.ToString());
            //radioButton.Name = newName;

            //FrameworkElement elem = this.FindName(newName) as FrameworkElement;
            //if (elem != null)
            //{
            //    RadioButton found = FindName(newName) as RadioButton; 
            //    Debug.WriteLine(found);
            //}

            RadioButton radioButton = sender as RadioButton;
            radioButton.Name = CreateValidIdentifier(radioButton.Content.ToString());





            // this works
            //RadioButton x = sender as RadioButton;
            //x.Name = "idk";



            //RadioButton b = FindName("idk") as RadioButton;
            //b.Background = new SolidColorBrush(Colors.Green);
        }
    }
}
