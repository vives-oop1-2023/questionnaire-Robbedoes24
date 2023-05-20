using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
