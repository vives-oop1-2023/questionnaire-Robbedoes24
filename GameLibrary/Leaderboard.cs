using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Leaderboard
    {
        public void AddEntry(string playerName, int playerPoints)
        {
            entries.Add(new Player(playerName, playerPoints));
        }

        public void AddEntry(Player score)
        {
            entries.Add(score);
        }

        public void SortEntries()
        {
            entries.Sort((x, y) => y.Score.CompareTo(x.Score));
        }

        public int CountEntries()
        {
            return entries.Count;
        }

        public List<Player> TopEntries(int amount)
        {
            // Make sure entries are sorted
            SortEntries();

            // Return all entries if requested amount is lower than available amount
            if (amount >= entries.Count)
            {
                return entries;
            }

            // Create new list
            List<Player> topEntries = new List<Player>();
            // Add top entries
            for (int i = 0; i < amount; i++)
            {
                topEntries.Add(entries[i]);
            }
            // return topEntries list
            return topEntries;
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return;
            }

            try
            {
                string jsonString = File.ReadAllText(filename);
                entries = JsonSerializer.Deserialize<List<Player>>(jsonString);
            }
            catch { return; }
        }

        public void SaveToFile(string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(entries, options);

            File.WriteAllText(filename, jsonString);
        }

        private List<Player> entries = new List<Player>();
    }
}
