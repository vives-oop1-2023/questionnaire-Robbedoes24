using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Player
    {
        public Player() { }

        public Player(string playerName)
        {
            Name = playerName;
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

        public string Name { get; private set; }
        public int Score { get; private set; }
    }
}
