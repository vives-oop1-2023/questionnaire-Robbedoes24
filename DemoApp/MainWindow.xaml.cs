﻿using DemoApp.Pages;
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
    public partial class MainWindow : Window, IQuestionHandler
    {
        public MainWindow()
        {
            InitializeComponent();
            // Go to the mainpage
            frame.NavigationService.Navigate(mainPage);
            // subscribe to event from mainPage and call methode when event is called (wait till start button is pressed)
            mainPage.StartGame += new EventHandler(StartGame);
        }

        private void StartGame(object sender, EventArgs e)
        {
            // Initialize data
            questionNumber = 0;

            // Create first question
            NextQuestion();
        }

        private void NextQuestion()
        {
            // Increase question number
            questionNumber++;


            // Create question

            Question question = new Question($"{questionNumber}: Is this a test?");
            question.AddAnswer(new Answer("Yes", true));
            question.AddAnswer(new Answer("No", false));
            question.AddAnswer(new Answer("How would i know", false));
            question.AddAnswer(new Answer("Maybe", false));

            // Create new question page and navigate to it
            questionPage = new QuestionPage(question);
            frame.NavigationService.Navigate(questionPage);

            // subscribe to event from questionpage and call methode when event is called (wait till question is answered)
            questionPage.QuestionAnswered += new EventHandler(GameLoop);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (questionNumber >= maxQuestions)
            {
                EndGame();
            }
            else
            {
                NextQuestion();
            }
        }

        private void EndGame()
        {
            // save score to file

            // go to endpage
            frame.NavigationService.Navigate(endPage);

            // subscribe to event from endpage and call methode when event is called (wait till mainmenu button is pressed)
            endPage.ToMainMenu += new EventHandler(ResetGame);
        }

        private void ResetGame(object sender, EventArgs e)
        {
            // Reset game data
            questionNumber = 0;

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
        private readonly int maxQuestions = 4;
        private int questionNumber = 0;

        // Store pages
        private QuestionPage questionPage; // TODO: Fix not nullable
        private readonly EndPage endPage = new EndPage();
        private readonly MainPage mainPage = new MainPage();
        //private readonly AboutPage aboutPage = new AboutPage();
    }
}
