using DemoApp.Pages;
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
using TriviaApiLibrary;

namespace WPFFractionCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Go to the mainpage
            frame.NavigationService.Navigate(mainPage);
            // subscribe to event from mainPage and call methode when event is called (wait till start button is pressed)
            mainPage.StartGame += new EventHandler(GameLoop);
        }

        private void StartGame(object sender, EventArgs e)
        {
            // Sets difficulty
            // TODO: Get difficulty from start page

            // Create first question
            NextQuestion();
        }

        private async void NextQuestion()
        {
            // Fetch question and add handler
            Question question = new Question();
            await TriviaApiRequester.RequestRandomQuestion(question);

            // Randomize question order
            question.RandomizeAnswers();

            // Increase question number
            questionNumber++;

            // Create new question page and navigate to it
            questionPage = new QuestionPage(question);
            frame.NavigationService.Navigate(questionPage);

            // subscribe to event from questionpage and call methode when event is called (wait till question is answered)
            questionPage.QuestionAnswered += new EventHandler<QuestionAnsweredEventArgs>(GameLoop);
        }

        private void GameLoop(object sender, QuestionAnsweredEventArgs e)
        {
            if (questionNumber == 0)
            {
                // Get first question
                NextQuestion();
            }
            else if ()
            // Wait 2 seconds so the user can see if answer was right or not 
            //if(questionNumber != 1)
            //{
            //    await Task.Delay(2000);
            //}

            // TODO: Get is answer was correct from question page and add points for player
            if(e.AnsweredCorrectly)
            {
                playerPoints += pointsPerCorrectAnswer;
            }

            // See if we should give a new question or end the game
            if (questionNumber < amountOfQuestions)
            {
                NextQuestion();
            }
            else // end the game
            {
                // save score to file

                // go to endpage
                endPage = new EndPage(playerPoints);
                frame.NavigationService.Navigate(endPage);

                // subscribe to event from endpage and call methode when event is called (wait till mainmenu button is pressed)
                endPage.ToMainMenu += new EventHandler(ResetGame);
            }
        }

        private void ResetGame(object sender, EventArgs e)
        {
            // Reset game data
            questionNumber = 0;
            playerName = "";
            playerPoints = 0;

            // Go to the mainpage
            frame.NavigationService.Navigate(mainPage);
            // subscribe to event from mainPage and call methode when event is called (wait till startbutton is pressed)
            mainPage.StartGame += new EventHandler(StartGame);
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            /*
            if (frame.NavigationService.Content is CalculatorPage)
            {
                frame.NavigationService.Navigate(aboutPage);
                about.Content = "Landingpage";
            }
            else
            {
                frame.NavigationService.Navigate(landingPage);
                about.Content = "About";
            }
            */
        }

        // Game data
        private int questionNumber = 0;
        private int playerPoints = 0;
        private string playerName = "";

        // Game settings
        private int pointsPerCorrectAnswer = 100;
        private readonly int amountOfQuestions = 4;

        // Store pages
        private QuestionPage questionPage; // TODO: Fix not nullable
        private EndPage endPage; // TODO: Fix not nullable
        private readonly MainPage mainPage = new MainPage();
        //private readonly AboutPage aboutPage = new AboutPage();




        private void GameLoop()
        {



        }






    }
}
