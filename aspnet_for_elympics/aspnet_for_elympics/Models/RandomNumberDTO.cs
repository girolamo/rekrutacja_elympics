using Newtonsoft.Json;

namespace Aspnet_for_elympics.Models
{
    public class RandomNumberDTO
    {
        [JsonProperty("randomNumber")]
        public int Value { get; set; }
    }
}
