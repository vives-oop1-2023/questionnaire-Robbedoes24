using GameLibrary;
using TriviaApiLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* The game library consists of the following classes:
            /   - Answer
            /   - Question
            /   - Player
            /   - Leaderboard
            /   - Game
            */

            // This Console app is made to show you the basics of what these classes have to offer.
            // Every class has a methode below to show some of their functionality.
            // You can add the methode below this line to see the console output.
            GameClass();
        }

        static void AnswerClass()
        {
            // When creating a new answer, the answer (string) and if the answer is correct (bool) are required
            Answer answer = new Answer("This is a test", true);

            // When an answer is constructed we can get the following data from it:
            // - The text
            Console.WriteLine($"Answer Text: {answer.Text}");
            // - If the answer is correct
            Console.WriteLine($"Answer IsCorrect: {answer.IsCorrect}");

            // The answer class also has the ToString methode 
            // This will return the answer text
            Console.WriteLine($"Answer (ToString): {answer}");
        }

        static void QuestionClass()
        {
            // When creating a new question, you have 2 options:
            // - Create an empty question
            Question emptyQuestion = new Question();
            // - Give it the question text
            Question question = new Question("Is This a Test?");

            // Now that we have a question, we can add answers (lets add 4)
            question.AddAnswer(new Answer("Yes", true));
            question.AddAnswer(new Answer("No", false));
            question.AddAnswer(new Answer("Maybe", false));
            question.AddAnswer(new Answer("Pass", false));

            // We can now look how many answers are in our question
            int amountOfAnswers = question.CountAnswers();
            Console.WriteLine($"Amount of answers: {amountOfAnswers}");

            // We can also remove an answer (lets remove the last one)
            question.RemoveAnswer(3);

            // We can request a certain question (with the index)
            // Note: If you give an incorrect index this will just return null
            Answer answer = question.GetAnswer(0);

            // You can also look if the question contains a specific answer
            // Note: we don't look at the answer text, but at the object itself (it must be the identical answer)
            bool containsAnswer = question.ContainsAnswer(answer);
            Console.WriteLine($"Contains the answer: {containsAnswer}");

            // There is also a methode to shuffle the answers
            question.ShuffleAnswers();

            // The question class also implements the ToString methode
            // This will return a the question and its answers in a formated message
            Console.WriteLine($"Question: {question}");

            // The question class has 2 properties that can be set or get
            // - The text (if you didn't give it with the constructor)
            question.Text = "Should This be a Test?";
            // - If the question has been answered (bool), should be set by the user.
            question.Answered = true;
        }

        static void PlayerClass()
        {
            // When creating a new player, you have 3 options:
            // - Create an empty player
            Player emptyPlayer = new Player();
            // - Give the player a name
            Player player = new Player("Dave");
            // - Give the player a name and some score
            Player scorePlayer = new Player("Steve", 1000);

            // Now that we have a player we can change its name
            player.ChangeName("Jef");

            // Give him some score
            player.AddScore(2000);

            // Remove some of his score
            player.RemoveScore(1000);

            // Or reset his score to 0
            player.ResetScore();

            // The player class also implements the ToString methode
            Console.WriteLine($"Player: {player}");

            // The player class has 2 properties that can be set or get
            // Note: it is not recomended to set these properties,
            // please use the methodes above to manipulate these properties instead.
            // - The players name
            Console.WriteLine($"Player name: {player.Name}");
            // - The players score
            Console.WriteLine($"Player score: {player.Score}");
        }

        static void LeaderboardClass()
        {
            // When creating a new leaderboard, you have 1 options:
            // - Create an empty leaderboard
            Leaderboard leaderboard = new Leaderboard();

            // Now we can add entries to the leaderboard
            // - Using the player class
            leaderboard.AddEntry(new Player("Jef", 1000));
            // - With a string and int
            leaderboard.AddEntry("Dave", 2000);

            // We can now count the amount of entries on the leaderboard
            int amountOfEntries = leaderboard.CountEntries();

            // We can also sort the leaderboard (most point 1st, ...)
            leaderboard.SortEntries();

            // You can also get the top n player of the leaderboard
            // Note: this will return an sorted list of players
            // If the amount exeeds the amount of entries, it will just return the available entries
            List<Player> topPlayers = leaderboard.TopEntries(5);

            // The leaderboard class also implements the ToString methode
            Console.WriteLine($"Leaderboard\n{leaderboard}");

            // There are 2 more methodes:
            // - load leaderboard from file (will try to load the leaderboard at the given filepath, relative to the eexecution)
            leaderboard.LoadFromFile("");
            // - save leaderboard to file (will save the leaderboard to a json file at the given filepath, relative to the eexecution)
            leaderboard.SaveToFile("");
        }

        static async void GameClass()
        {
            // The game class is a more advanced class
            // First we should create a new game, this will create a game and put its state on unknown
            Game game = new Game();

            // If our gamestate is unknown, we can proceed
            if(game.State != GameState.unknown) { return; }
            Console.WriteLine("Game Created");

            // Now we ca start the game, we need to give it a difficulty and a playername and optionaly an amount of questions
            game.Start(Difficulty.easy, "Steve", 1);

            // Now we need to check if the state has swithed to uninitialized
            if (game.State != GameState.uninitialized) { return; }
            Console.WriteLine("Game Started");

            // Now that the game has started, we need to initialize it, there a 2 ways to do this:
            // - giving a list of questions
            List<Question> questions = new List<Question>();
            Question exampleQuestion = new Question("Is this an example?");
            exampleQuestion.AddAnswer(new Answer("Yes", true));
            exampleQuestion.AddAnswer(new Answer("No", false));
            questions.Add(exampleQuestion);

            game.Init(questions);

            // - giving it an api to request questions (not working in this example)
            //game.Init(new TriviaApi());

            // Now we need to check if the state has swithed to initialized
            if (game.State != GameState.initialized) { return; }
            Console.WriteLine("Game Ready");

            // Now that the game is ready, we can ask for a question
            // Note: the question will be null and game.state will be ended if there are no more questions to give
            Question question = game.GetQuestion();
            Console.WriteLine($"Question: {question}");

            // Can now answer the question (is the answer is correct our score will increase
            game.SubmitAnswer(question.GetAnswer(0));
            Console.WriteLine("Answer Submited");

            // We only asked for 1 question so if we ask for a question again, we get null and the game.state will be ended
            question = game.GetQuestion();
            if (question != null || game.State != GameState.ended) { return; }
            Console.WriteLine("Game Has Ended");

            // Now we can get the leaderboard.
            // note: this is saved to file every time the game ends, and loaded every time a game is initialized
            Leaderboard leaderboard = game.GetLeaderboard();
            // clear leaderboard and only add top 3 players
            List<Player> topPlayers = leaderboard.TopEntries(3);
            leaderboard = new Leaderboard();
            foreach (Player player in topPlayers)
            {
                leaderboard.AddEntry(player);
            }
            Console.WriteLine($"Leaderboard:\n{leaderboard}");
        }
    }
}