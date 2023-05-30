using Newtonsoft.Json;

namespace TriviaApiLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TriviaQuestion
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
