using System.Text.Json;

namespace ScoreBoardLibrary
{
    public class ScoreBoard
    {
        public void AddScore(string playerName, int playerPoints)
        {
            scores.Add(new Score(playerName, playerPoints));
        }

        public void AddScore(Score score)
        {
            scores.Add(score);
        }

        public void SortScores()
        {
            scores.Sort((x,y) => y.Points.CompareTo(x.Points));        
        }

        public int CountScores()
        { 
            return scores.Count; 
        }

        public override string ToString()
        {
            return TopScoresString(scores.Count);
        }

        public string TopScoresString(int amountOfScores)
        {
            // Make sure scores are sorted
            SortScores();

            // if amountOfScores is greater that the amount of scores, return only available scores
            if (amountOfScores > scores.Count) 
            {
                amountOfScores = scores.Count;
            }

            // Create output string
            string output = "";
            // Add playername and score of top amountOfScores
            for (int i = 0; i < amountOfScores; i++)
            {
                string playerName = scores[i].Name;
                int playerPoints = scores[i].Points;
                if (i > 0 && i < amountOfScores)
                {
                    output += "\n";
                }
                output += $"{i + 1}. {playerName} {playerPoints}";
            }
            // Return output string
            return output;
        }

        public List<Score> TopScores(int amountOfScores)
        {
            // Make sure scores are sorted
            SortScores();

            // Return Score list if amount is less or equal to asked amout
            if (amountOfScores >= scores.Count)
            {
                return scores;
            }

            // Create new score list
            List<Score> topScores = new List<Score>();
            // Add top amountOfScores scores
            for (int i = 0; i < amountOfScores;i++)
            {
                topScores.Add(scores[i]);
            }
            // return topscores list
            return topScores;
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
                scores = JsonSerializer.Deserialize<List<Score>>(jsonString);
            }
            catch { return; }
        }

        public void SaveToFile(string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(scores, options);

            File.WriteAllText(filename, jsonString);
        }

        private List<Score> scores = new List<Score>();
    }
}
