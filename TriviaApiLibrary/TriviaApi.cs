using GameLibrary;

namespace TriviaApiLibrary
{
    public class TriviaApi : IQuestionApi
    {
        public async Task GetRandomQuestion(Question question, Difficulty difficulty)
        {
            TriviaQuestionConverter converter = new TriviaQuestionConverter(question);
            await TriviaApiRequester.RequestRandomQuestion(converter, difficulty);
        }
    }
}
