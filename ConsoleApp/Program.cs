using QuestionnaireLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Question question1 = new Question("test");

            question1.AddAnswer(new Answer("a", true));
            question1.AddAnswer(new Answer("b", false));
            question1.AddAnswer(new Answer("c", false));
            question1.AddAnswer(new Answer("d", false));

            Console.WriteLine(question1);

            int amount_of_answers = question1.CountAnswers();
            Console.WriteLine($"Amount of answers: {amount_of_answers}");

            for (int i = 0; i < amount_of_answers; i++)
            {
                Answer? answer = question1.GetAnswer(i);
                if (answer.IsCorrect == true)
                {
                    Console.WriteLine($"Correct: {answer}");
                }
                Console.WriteLine($"Incorrect: {answer}");
            }
        }
    }
}