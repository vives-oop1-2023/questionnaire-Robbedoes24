
namespace GameLibrary
{
    public interface IQuestionApi
    {
        public Task GetRandomQuestion(Question question, Difficulty difficulty);
    }
}
