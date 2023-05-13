using QuestionnaireLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFFractionCalculator;

namespace DemoApp.Pages
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
                if (answer == null){break;}

                // add a new button to answer list stackpanel
                Button button = new Button();

                button.Name = $"answer_{i}";
                button.Content = answer.Text;
                button.Tag = answer;
                button.Click += new RoutedEventHandler(OnClickAnswer);

                AnswerList.Children.Add(button);
            }
        }

        void OnClickAnswer(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            // TODO: Return exeption if button is null
            
            Answer? answer = button.Tag as Answer;
            // TODO: Return exeption if answer is null

            if (answer.IsCorrect)
            {
                // add points to player score

                // Change background to green
                button.Background = this.FindResource("Button.Answer.Right") as SolidColorBrush; // TODO Test
            }
            else
            {
                // change background to red
                button.Background = this.FindResource("Button.Answer.Wrong") as SolidColorBrush;
            }

            // Call question answered event
            QuestionAnswered(this, e);
        }

        // Create event that will be called when question is answered
        public event EventHandler QuestionAnswered;
    }
}
