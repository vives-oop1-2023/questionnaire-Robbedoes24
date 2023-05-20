using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public interface IQuestionApi
    {
        public Task GetRandomQuestion(Question question, Difficulty difficulty);
    }
}
