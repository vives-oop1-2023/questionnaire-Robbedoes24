using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApiLibrary;

namespace GameLibrary
{
    public class Game
    {
        public async void Initialize()
        {
            // load previous scoreboard data
            scoreBoard.LoadFromFile(scoreBoardFilePath);

            // Get all questions
            for (int i = 0; i < amountOfQuestions; i++)
            {

                TriviaApiRequester.RequestRandomQuestion(new Question(), difficulty);
            }


            TriviaApiRequester.RequestRandomQuestion();
        }

        public Question GetNextQuestion()
        {




        }
        
        public void Start(string playerName)
        {
            Player.ChangeName(playerName);

        }

        public void Stop()
        {


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

        private int currentQuestion = 0;
        private Difficulty difficulty = Difficulty.easy;
        private List<Question> questions = new List<Question>();
        private Scoreboard scoreBoard = new Scoreboard();


        public Player Player { get; set; }
        
        public GameState State { get; private set; }

        private List<Question> questions = new List<Question>();
    }
}
