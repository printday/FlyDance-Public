namespace Common.Models.Responses
{
    /// <summary>
    /// Ai对话返回
    /// </summary>

    public class Choice
    {
        public Message message { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
        public PromptTokensDetails prompt_tokens_details { get; set; }
    }

    public class PromptTokensDetails
    {
        public int cached_tokens { get; set; }
    }

    public class Root
    {
        public List<Choice> choices { get; set; }
        public string @object { get; set; }
        public Usage usage { get; set; }
        public long created { get; set; }
        public object system_fingerprint { get; set; }
        public string model { get; set; }
        public string id { get; set; }
    }
}
