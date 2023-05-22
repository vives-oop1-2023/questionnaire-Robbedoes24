using GameLibrary;
using QuestionnaireApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Windows.Threading;
using System.Xml.Linq;
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
            // Create new game
            game = new Game();
            // set the timer interval (Update rate of approximately 60 FPS)
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16);
            // Add event handler for tick event of timer
            gameLoopTimer.Tick += GameLoopTick;
            // start the timer
            gameLoopTimer.Start();
        }

        private async void GameLoopTick(object sender, EventArgs e)
        {
            // This methode runs every tick (approximately 60 time a second, with current interval)
            if (game.State == GameState.unknown && appState != AppState.starting)
            {
                // Set state of app to starting
                appState = AppState.starting;
                // Reset TaskCompletionSource
                startEventSource = new TaskCompletionSource<bool>();
                // Show and enable about button
                AboutButton.Visibility = Visibility.Visible;
                AboutButton.IsEnabled = true;
                // Create new start page
                StartPage startPage = new StartPage(difficulty);
                // Store startpage in temp variable (for use with about button)
                this.startPage = startPage;
                // Go to the startpage
                PageFrame.NavigationService.Navigate(startPage);
                // Add new eventhandler for startpage
                startPage.StartGame += new EventHandler<StartGameEventArgs>(ProcessStartEvent);
                // Wait for eventhandler to be called
                await startEvent;
            }

            if (game.State == GameState.uninitialized && appState != AppState.loading)
            {
                // Set state of app to loading
                appState = AppState.loading;
                // Create new loading page
                LoadingPage loadingPage = new LoadingPage();
                // Go to the loadingpage
                PageFrame.NavigationService.Navigate(loadingPage);
                // Get Questions
                game.Init(new TriviaApi()); //TODO should be awaited
                // Wait for an extra second
                await Task.Delay(1000);
            }

            if (game.State == GameState.initialized && appState != AppState.answering)
            {
                // Set state of app to answering
                appState = AppState.answering;
                // Get question from game
                Question? question = game.GetQuestion();
                // If no question is given, exit
                if (question == null)
                {
                    return;
                }
                // Reset TaskCompletionSource
                questionEventSource = new TaskCompletionSource<bool>();
                // Create new question page
                QuestionPage questionPage = new QuestionPage(question); //TODO fetch question
                // Go to the questionpage
                PageFrame.NavigationService.Navigate(questionPage);
                // Add new eventhandler for questionpage
                questionPage.QuestionAnswered += new EventHandler<QuestionAnsweredEventArgs>(ProcessQuestionEvent);
                // Wait for eventhandler to be called
                await questionEvent;
            }

            if (game.State == GameState.ended && appState != AppState.ending)
            {
                // Set state of app to ending
                appState = AppState.ending;
                // Reset TaskCompletionSource
                endEventSource = new TaskCompletionSource<bool>();
                // Get Leaderboard from game
                Leaderboard leaderboard = game.GetLeaderboard();
                // Create new end page
                EndPage endPage = new EndPage(leaderboard.TopEntries(amountOfScores));
                // Go to the endpage
                PageFrame.NavigationService.Navigate(endPage);
                // subscribe to event from endpage and call methode when event is called (wait till main menu button is pressed)
                endPage.ToMainMenu += new EventHandler(ProcessEndEvent);
                // Wait for eventhandler to be called
                await endEvent;
            }
        }

        private void ProcessStartEvent(object sender, StartGameEventArgs e)
        {
            // Create a new game
            game.Start(e.Difficulty, e.PlayerName);

            // Set local difficulty value
            difficulty = e.Difficulty;

            // Hide and disable about button
            AboutButton.Visibility = Visibility.Collapsed;
            AboutButton.IsEnabled = false;

            // Mark task as done
            startEventSource.SetResult(true);
        }

        private void ProcessQuestionEvent(object sender, QuestionAnsweredEventArgs e)
        {
            // Submit answer
            game.SubmitAnswer(e.SelectedAnswer);

            // Mark task as done
            questionEventSource.SetResult(true);

            // Set state of app to answered
            appState = AppState.answered;
        }

        private void ProcessEndEvent(object sender, EventArgs e)
        {
            // create new game
            game = new Game();

            // Mark task as done
            endEventSource.SetResult(true);
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            if (PageFrame.NavigationService.Content == this.startPage)
            {
                PageFrame.NavigationService.Navigate(this.aboutPage);
                AboutButton.Content = "Back";
            }
            else
            {
                PageFrame.NavigationService.Navigate(this.startPage);
                AboutButton.Content = "About";
            }
        }

        // TaskCompletionSources so we can wait till eventhandler has been executed
        private static TaskCompletionSource<bool> startEventSource = new TaskCompletionSource<bool>();
        private static TaskCompletionSource<bool> questionEventSource = new TaskCompletionSource<bool>();
        private static TaskCompletionSource<bool> endEventSource = new TaskCompletionSource<bool>();
        Task<bool> startEvent = startEventSource.Task;
        Task<bool> questionEvent = startEventSource.Task;
        Task<bool> endEvent = startEventSource.Task;

        // Game/app variables
        private readonly int amountOfScores = 5;
        private Difficulty difficulty = Difficulty.easy;
        private Game game = new Game();
        private AppState appState = AppState.unknown;
        DispatcherTimer gameLoopTimer = new DispatcherTimer();
        private readonly AboutPage aboutPage = new AboutPage();

        // Temporary variables
        private StartPage startPage = new StartPage(Difficulty.easy);

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            gameLoopTimer.Stop();
        }
    }
}
