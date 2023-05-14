using QuestionnaireApp.Pages;
using QuestionnaireLibrary;
using ScoreBoardLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
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

namespace QuestionnaireApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Load previous scores into scoreboard (from file)
            scoreBoard.LoadFromFile(scoreBoardFilePath);
            // Add Handler for window close event
            Closing += MainWindow_Closing;
            // Start Gameloop
            GameLoop();
        }

        private async void GameLoop()
        {
            // Loop Game logic till window is closed
            while (true)
            {
                if (currentQuestion == 0)
                {
                    await StartGame();
                    await LoadData();
                    currentQuestion = 1;
                }
                else if (currentQuestion <= amountOfQuestions)
                {
                    await NextQuestion();
                    currentQuestion++;
                }
                else
                {
                    await EndGame();
                    // Reset Game Data
                    ResetGameData();
                }
            }
        }

        private Task<bool> StartGame()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            // Methode to process StartPageEvents
            void ProcessStartEvent(object sender, StartGameEventArgs e)
            {
                // Create new score with playername
                score = new Score(e.PlayerName);
                // Set difficulty to selected difficulty
                difficulty = e.Difficulty;
                // Mark task as done
                tcs.SetResult(true);
            }

            // Create new start page
            StartPage startPage = new StartPage(difficulty);
            // Go to the startpage
            PageFrame.NavigationService.Navigate(startPage);
            // Add new eventhandler for startpage
            startPage.StartGame += new EventHandler<StartGameEventArgs>(ProcessStartEvent);
            // Wait till event is invoked (button is pressed)
            return tcs.Task;
        }

        private async Task LoadData()
        {
            // Create new loading page
            LoadingPage loadingPage = new LoadingPage();
            // Go to the loadingpage
            PageFrame.NavigationService.Navigate(loadingPage);

            // Get Questions
            await FetchQuestions();

            return;
        }

        private async Task FetchQuestions()
        {
            // Fetch a question for amountOfQuestions
            for (int i = 0; i < amountOfQuestions; i++)
            {
                // Create new empty question
                Question question = new Question();
                // Fetch
                await TriviaApiRequester.RequestRandomQuestion(question, difficulty);
                question.RandomizeAnswers();

                questions.Add(question);
            }
            return;
        }

        private Task<bool> NextQuestion()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            // Methode to process QuestionPageEvents
            void ProcessQuestionEvent(object sender, QuestionAnsweredEventArgs e)
            {
                // If anwsered correctly
                if (e.AnsweredCorrectly)
                {
                    // Add points based on the difficulty
                    switch (difficulty)
                    {
                        case Difficulty.easy:
                            score.AddPoints(Convert.ToInt32(scoreCorrectAnswer * easyScoreMultiplier));
                            break;
                        case Difficulty.medium:
                            score.AddPoints(Convert.ToInt32(scoreCorrectAnswer * mediumScoreMultiplier));
                            break;
                        case Difficulty.hard:
                            score.AddPoints(Convert.ToInt32(scoreCorrectAnswer * hardScoreMultiplier));
                            break;
                        default:
                            break;
                    }
                }
                // Mark task as done
                tcs.SetResult(true);
            }

            // Create new question page
            QuestionPage questionPage = new QuestionPage(questions[currentQuestion-1]); //TODO fetch question
            // Go to the questionpage
            PageFrame.NavigationService.Navigate(questionPage);
            // Add new eventhandler for questionpage
            questionPage.QuestionAnswered += new EventHandler<QuestionAnsweredEventArgs>(ProcessQuestionEvent);
            // Wait till event is invoked (question is answered)
            return tcs.Task;
        }

        private Task<bool> EndGame()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            // Methode to process EndPageEvents
            void ProcessEndEvent(object sender, EventArgs e)
            {
                // We don't get any data from the endpage, so we do nothing here.
                // Mark task as done
                tcs.SetResult(true);
            }

            // Add score to scoreboard
            scoreBoard.AddScore(score);
            // Create new end page
            EndPage endPage = new EndPage(scoreBoard.TopScores(scoreboardScores));
            // Go to the endpage
            PageFrame.NavigationService.Navigate(endPage);
            // subscribe to event from endpage and call methode when event is called (wait till main menu button is pressed)
            endPage.ToMainMenu += new EventHandler(ProcessEndEvent);
            // Wait till event is invoked (button is pressed)
            return tcs.Task;
        }

        private void ResetGameData()
        {
            score = new Score();
            currentQuestion = 0;
            questions = new List<Question>();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Save Scoreboard data to file
            if (scoreBoard.CountScores() > 0)
            {
                scoreBoard.SaveToFile(scoreBoardFilePath);
            }
        }

        // Game Settings
        private readonly int amountOfQuestions = 4;
        private readonly int scoreboardScores = 5;
        private readonly int scoreCorrectAnswer = 100;
        private readonly double easyScoreMultiplier = 0.5;
        private readonly double mediumScoreMultiplier = 1;
        private readonly double hardScoreMultiplier = 1.5;
        private readonly string scoreBoardFilePath = "scoreboard.json";

        // Game Data
            // Should be reset on game end
        private Score score = new Score();
        private int currentQuestion = 0;
        private List<Question> questions = new List<Question>();
            // Should not be cleared on game end
        private ScoreBoard scoreBoard = new ScoreBoard();
        private Difficulty difficulty = Difficulty.easy;
    }
}
