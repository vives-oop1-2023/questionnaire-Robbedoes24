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
            return $"{Name}: {Points}";
        }

        public void AddPoints (int points)
        {
            Points += points;
        }

        public string Name { get; set; }
        public int Points { get; set; }
    }
}
