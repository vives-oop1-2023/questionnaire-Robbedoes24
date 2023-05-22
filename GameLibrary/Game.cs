using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Game
    {
        public Game()
        {
            State = GameState.unknown;
        }

        public void Start(Difficulty difficulty, string playername)
        {
            if (State != GameState.unknown)
            {
                throw new Exception("Can't start a game that is already started!");
            }

            this.difficulty = difficulty;
            player = new Player(playername);

            // Set state to uninitialized so the app knows it needs to initialize the game
            State = GameState.uninitialized;
        }

        public async void Init(IQuestionApi api)
        {
            if (State != GameState.uninitialized)
            {
                throw new Exception("Shouldn't initialize a game that is already initialized!");
            }

            // Fetch all questions
            for (int i = 0; i < amountOfQuestions; i++)
            {
                Question question = new Question(); 
                await api.GetRandomQuestion(question, difficulty);
                questions.Add(question);
            }

            // Load previous scores from file (if exist)
            leaderboard.LoadFromFile(LeaderboardSavePath);

            // Set current question to 0
            questionCounter = 0;

            // Set state to initialized so the app knows the game can be played
            State = GameState.initialized;
        }

        public Question? GetQuestion()
        {
            if (State != GameState.initialized)
            {
                throw new Exception("Can't get question if game isn't initialized!");
            }

            if (questionCounter < amountOfQuestions)
            {
                return questions[questionCounter];
            }

            State = GameState.ended;
            return null;
        }

        public void SubmitAnswer(Answer answer)
        {
            if (questions[questionCounter].Answered == true)
            {
                throw new Exception("Can't submit an answer for a question that has already been answered!");
            }

            if (questions[questionCounter].ContainsAnswer(answer) && answer.IsCorrect == true)
            {
                switch (difficulty)
                {
                    case Difficulty.easy:
                        player.AddScore(Convert.ToInt32(scoreCorrectAnswer * easyScoreMultiplier));
                        break;
                    case Difficulty.medium:
                        player.AddScore(Convert.ToInt32(scoreCorrectAnswer * mediumScoreMultiplier));
                        break;
                    case Difficulty.hard:
                        player.AddScore(Convert.ToInt32(scoreCorrectAnswer * hardScoreMultiplier));
                        break;
                    default:
                        break;
                }
            }

            // mark current question as answered
            questions[questionCounter].Answered = true;

            // increment the question counter
            questionCounter++;
        }

        public Leaderboard GetLeaderboard()
        {
            if (State != GameState.ended)
            {
                throw new Exception("Can't get score of a game that hasn't ended yet!");
            }

            // Add player to Leaderboard
            leaderboard.AddEntry(player);

            // Save Leaderboard to file
            leaderboard.SaveToFile(LeaderboardSavePath);

            return leaderboard;
        }

        // Game Settings
        private readonly int amountOfQuestions = 10;
        private readonly int scoreCorrectAnswer = 100;
        private readonly double easyScoreMultiplier = 0.5;
        private readonly double mediumScoreMultiplier = 1;
        private readonly double hardScoreMultiplier = 1.5;
        private readonly string LeaderboardSavePath = "Leaderboard.json";

        // Game Data
        private Difficulty difficulty = Difficulty.easy;
        private Player player = new Player();
        private List<Question> questions = new List<Question>();
        private int questionCounter = 0;
        private Leaderboard leaderboard = new Leaderboard();

        public GameState State { get; private set; }
    }
}
