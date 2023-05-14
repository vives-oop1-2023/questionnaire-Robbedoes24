using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoardLibrary
{
    public class Score
    {
        public Score() { }

        public Score(string playerName) 
        {
            Name = playerName;
        }

        public Score(string playerName, int playerPoints)
        {
            Name = playerName;
            Points = playerPoints;
        }

        public override string ToString()
        {
            return $"{name} {points}";
        }

        public void AddPoints (int points)
        {
            Points += points;
        }

        public string? Name { get; private set; }
        public int Points { get; private set; }

        private string name = "";
        private int points = 0;
    }
}
