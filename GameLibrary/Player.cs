
namespace GameLibrary
{
    public class Player
    {
        public Player() 
        { 
            Name = string.Empty;
            Score = 0;
        }

        public Player(string playerName)
        {
            Name = playerName;
            Score = 0;
        }

        public Player(string playerName, int playerScore)
        {
            Name = playerName;
            Score = playerScore;
        }

        public override string ToString()
        {
            return $"{Name}: {Score}";
        }

        public void ChangeName(string name)
        { 
            Name = name ; 
        }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void RemoveScore(int score)
        {
            Score -= score;
        }

        public void ResetScore()
        {
            Score = 0 ;
        }

        public string Name { get; set; }
        public int Score { get; set; }
    }
}
