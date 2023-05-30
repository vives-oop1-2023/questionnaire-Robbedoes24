
namespace GameLibrary
{
    public class Game
    {
        public Game()
        {
            State = GameState.unknown;
        }

        public void Start(Difficulty difficulty, string playername, int amountOfQuestions = 10)
        {
            if (State != GameState.unknown)
            {
                throw new Exception("Can't start a game that is already started!");
            }

            this.difficulty = difficulty;
            player = new Player(playername);
            this.amountOfQuestions = amountOfQuestions;

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
            leaderboard.LoadFromFile(leaderboardSavePath);

            // Set current question to 0
            questionCounter = 0;

            // Set state to initialized so the app knows the game can be played
            State = GameState.initialized;
        }

        public void Init(List<Question> questions)
        {
            if (State != GameState.uninitialized)
            {
                throw new Exception("Shouldn't initialize a game that is already initialized!");
            }

            // Add all questions
            for (int i = 0; i < questions.Count; i++)
            {
                this.questions.Add(questions[i]);
            }

            // Randomize questions using the Fisher–Yates shuffle
            Random gen = new Random();
            // For the amount of items in questions
            for (int i = this.questions.Count - 1; i > 0; i--)
            {
                //get random index of remaning items
                int k = gen.Next(i + 1);
                // swap values
                (this.questions[i], this.questions[k]) = (this.questions[k], this.questions[i]);
            }

            // Load previous scores from file (if exist)
            leaderboard.LoadFromFile(leaderboardSavePath);

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

            // Add player to Leaderboard
            leaderboard.AddEntry(player);

            // Save Leaderboard to file
            leaderboard.SaveToFile(leaderboardSavePath);

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

            return leaderboard;
        }

        // Game Settings
        private readonly int scoreCorrectAnswer = 100;
        private readonly double easyScoreMultiplier = 0.5;
        private readonly double mediumScoreMultiplier = 1;
        private readonly double hardScoreMultiplier = 1.5;
        private readonly string leaderboardSavePath = "Leaderboard.json";

        // Game Data
        private int amountOfQuestions = 10;
        private Difficulty difficulty = Difficulty.easy;
        private Player player = new Player();
        private readonly List<Question> questions = new List<Question>();
        private int questionCounter = 0;
        private readonly Leaderboard leaderboard = new Leaderboard();

        public GameState State { get; private set; }
    }
}
