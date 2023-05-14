using TriviaApiLibrary;

namespace QuestionnaireLibrary
{
    public class Question : IQuestionHandler
    {
        public Question() { }

        public Question(string text)
        {
            Text = text;
        }

        public Question(string text, string imageUrl)
        {
            Text = text;
            ImageUrl = imageUrl;
        }

        public override string ToString()
        {
            string output = Text;
            foreach (var answer in answers)
            {
                output += $"\n - {answer.Text}";
            }
            return output;
        }

        public void AddAnswer(Answer answer)
        {
            answers.Add(answer);
        }

        public Answer? GetAnswer(int index)
        {
            if (index >= answers.Count)
            {
                return null;
            }

            return answers[index]; 
        }

        public int CountAnswers()
        { 
            return answers.Count;
        }

        public void RandomizeAnswers()
        {
            Random gen = new Random();
            for (int i = CountAnswers() - 1; i > 0; i--)
            {
                var k = gen.Next(i + 1);
                var value = answers[k];
                answers[k] = answers[i];
                answers[i] = value;
            }
        }

        // Implement ProcessQuestion methode of IQuestionHandler
        public void ProcessQuestion(TriviaMultipleChoiceQuestion jsonQuestion)
        {
            // set question text
            text = jsonQuestion.Question.Text;

            // Add correct answer to answers
            answers.Add(new Answer(jsonQuestion.CorrectAnswer, true));

            // Add incorrect answers to answers
            for (int i = 0; i < jsonQuestion.IncorrectAnswers.Count; i++)
            {
                answers.Add(new Answer(jsonQuestion.IncorrectAnswers[i], false));
            }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string ImageUrl
        {
            get { return imageURL; }
            set { imageURL = value; }
        }


        private string text = "";
        private string imageURL = "";
        private List<Answer> answers= new List<Answer>();
    }
}