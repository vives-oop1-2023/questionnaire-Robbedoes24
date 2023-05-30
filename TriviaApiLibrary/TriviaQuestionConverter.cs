using GameLibrary;

namespace TriviaApiLibrary
{
    public class TriviaQuestionConverter : IQuestionHandler
    {
        public TriviaQuestionConverter(Question question)
        {
            Question = question;
        }

        public void ProcessQuestion(TriviaMultipleChoiceQuestion triviaQuestion)
        {
            // set question text
            Question.Text = triviaQuestion.Question.Text;

            // Add correct answer to answers
            Question.AddAnswer(new Answer(triviaQuestion.CorrectAnswer, true));

            // Add incorrect answers to answers
            for (int i = 0; i < triviaQuestion.IncorrectAnswers.Count; i++)
            {
                Question.AddAnswer(new Answer(triviaQuestion.IncorrectAnswers[i], false));
            }
            
            // Shuffle answers
            Question.ShuffleAnswers();
        }

        private Question Question { get; set; }
    }
}
