
namespace GameLibrary
{
    public class Question
    {
        public Question() 
        { 
            Text = string.Empty;
        }

        public Question(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            string output = Text;
            foreach (var answer in answers)
            {
                output += $"\n- {answer}";
            }
            return output;
        }

        public void ShuffleAnswers()
        {
            // Randomize answers using the Fisher–Yates shuffle
            Random gen = new Random();
            // For the amount of items in annswers
            for (int i = answers.Count - 1; i > 0; i--)
            {
                //get random index of remaning items
                int k = gen.Next(i + 1);
                // Swap values
                (answers[i], answers[k]) = (answers[k], answers[i]);
            }
        }

        public Answer? GetAnswer(int index)
        {
            if (index >= answers.Count)
            {
                return null;
            }

            return answers[index];
        }

        public bool ContainsAnswer(Answer answer)
        {
            if (answers.Contains(answer))
            {
                return true;
            }
            return false;
        }

        public void AddAnswer(Answer answer)
        {
            answers.Add(answer);
        }

        public void RemoveAnswer(int index)
        {
            answers.RemoveAt(index);
        }

        public int CountAnswers()
        {
            return answers.Count;
        }

        public string Text { get; set; }

        public bool Answered { get; set; }

        private readonly List<Answer> answers = new List<Answer>();
    }
}
