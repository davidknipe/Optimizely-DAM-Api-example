using Newtonsoft.Json;

namespace Foundation.Features.OptimizelyDAM.REST.Media
{
    public class ImageResolution
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}