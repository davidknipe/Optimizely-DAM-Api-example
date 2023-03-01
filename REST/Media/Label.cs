using Newtonsoft.Json;

namespace Foundation.Features.OptimizelyDAM.REST.Media
{
    public class Label
    {
        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("values")]
        public List<GroupValue> Values { get; set; }
    }
}