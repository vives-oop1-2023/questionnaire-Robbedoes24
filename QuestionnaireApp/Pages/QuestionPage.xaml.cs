using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameLibrary;

namespace QuestionnaireApp.Pages
{
    /// <summary>
    /// Interaction logic for QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        public QuestionPage(Question question)
        {
            InitializeComponent();

            // Add question text to the Question textblock
            Question.Text = question.Text;

            // Create pass answer and add it to question
            Answer pass = new Answer("Pass", false);
            question.AddAnswer(pass);

            // Loop through all answers of the question
            for (int i = 0; i < question.CountAnswers(); i++)
            {
                // get answer from question
                Answer? answer = question.GetAnswer(i);

                // Break if answer is null
                if (answer == null) { break; }

                // add a new button to answer list stackpanel
                Button button = new Button();

                button.Name = $"answer_{i}";
                button.Content = answer.Text;
                button.Tag = answer;
                button.Click += new RoutedEventHandler(OnClickAnswer);
                button.Style = FindResource("AnswerButton") as Style;

                AnswerList.Children.Add(button);
            }
        }

        async void OnClickAnswer(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Answer clickedAnswer = clickedButton.Tag as Answer;

            //Disable all buttons (no extra clicks)
            Button? correctButton = new Button();
            Answer? correctAnswer = new Answer("",false);
            for (int i = 0; i < AnswerList.Children.Count; i++)
            {
                Button button = AnswerList.Children[i] as Button;
                button.IsEnabled = false;
                Answer answer = button.Tag as Answer;
                if (answer.IsCorrect)
                {
                    correctButton = button;
                    correctAnswer = answer;
                }
            }

            // Change button background if correct or incorrect and wait a few seconds
            if (clickedAnswer.IsCorrect)
            {
                clickedButton.Background = FindResource("QuestionPage.Answer.Background.Right") as SolidColorBrush;

                // wait x seconds to the user can see their answer was correct;
                await Task.Delay(baseWaitTime);
            }
            else
            {
                clickedButton.Background = FindResource("QuestionPage.Answer.Background.Wrong") as SolidColorBrush;
                correctButton.Background = FindResource("QuestionPage.Answer.Background.Right") as SolidColorBrush;

                // wait x seconds to the user can see and read the correct answer;
                await Task.Delay(baseWaitTime + (millisecondsPerLetter * correctAnswer.Text.Length));
            }

            // Call question answered event
            QuestionAnswered(this, new QuestionAnsweredEventArgs(clickedAnswer));
        }

        // Create eventhandler that will be called when question is answered
        public event EventHandler<QuestionAnsweredEventArgs> QuestionAnswered;

        private readonly int baseWaitTime = 500;
        private readonly int millisecondsPerLetter = 200;
    }

    public class QuestionAnsweredEventArgs : EventArgs
    {
        public QuestionAnsweredEventArgs(Answer selectedAnswer)
        {
            SelectedAnswer = selectedAnswer;
        }

        public Answer SelectedAnswer { get; private set; }
    }
}

