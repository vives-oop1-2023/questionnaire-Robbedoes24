﻿using Newtonsoft.Json;
using System.Diagnostics;
using GameLibrary;

namespace TriviaApiLibrary
{
    public class TriviaApiRequester
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        private static readonly HttpClient client = new HttpClient();

        private static TriviaMultipleChoiceQuestion ParseToQuestion(string question)
        {
            if (question == null) return null;

            TriviaMultipleChoiceQuestion result = null;
            try
            {
                result = JsonConvert.DeserializeObject<TriviaMultipleChoiceQuestion>(question);
            }
            catch (JsonSerializationException ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return result;
        }

        public static async Task RequestRandomQuestion(IQuestionHandler handler, Difficulty difficulty)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string responseBody = await client.GetStringAsync($"https://the-trivia-api.com/v2/questions?types=text_choice&difficulties={difficulty}&limit=1");

                Trace.WriteLine(responseBody);

                // Call the handler
                handler.ProcessQuestion(ParseToQuestion(
                        responseBody.Substring(1, responseBody.Length - 2)            // Extract object from pagination array
                    ));
            }
            catch (HttpRequestException e)
            {
                handler.ProcessQuestion(null);
            }
        }
    }
}
