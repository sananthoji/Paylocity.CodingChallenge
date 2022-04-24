using Newtonsoft.Json;

namespace Paylocity.CodingChallenge.Framework
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BusinessException : System.Exception
    {
        [JsonProperty("Code")]
        public string? Code { get; }
        [JsonProperty("Message")]
        public string ErrorMessage { get; }

        public BusinessException(string message) : base(message)
        {
            this.ErrorMessage = message;
        }

        public BusinessException(string code, string message) : base(message)
        {
            this.ErrorMessage = message;
            this.Code = code;
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
            this.ErrorMessage = message;
        }

        public BusinessException(string code, string message, Exception innerException) : base(message, innerException)
        {
            this.Code = code;
            this.ErrorMessage = message;
        }
    }
}
