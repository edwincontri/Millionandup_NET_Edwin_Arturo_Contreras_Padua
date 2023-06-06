using Newtonsoft.Json;

namespace Api_Properties.Models
{
    public class ResponseResult
    {
        [JsonProperty("isError")]
        public bool IsError { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
