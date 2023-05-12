using TriviaApiLibrary;

namespace QuestionnaireLibrary
{
    public class Question : IQuestionHandler
    {
        private Question() { }

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

        // Static methode that returns a question that has been fetched using the TriviaApiLibrary
        public static Question GetRandomQuestion()
        {
            Question question = new Question();
            await TriviaApiRequester.RequestRandomQuestion(question);
            return question;
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
            private set { text = value; }
        }

        public string ImageUrl
        {
            get { return imageURL; }
            private set { imageURL = value; }
        }


        private string text = "";
        private string imageURL = "";
        private List<Answer> answers= new List<Answer>();
    }
}