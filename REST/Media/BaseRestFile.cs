﻿using Newtonsoft.Json;

namespace Foundation.Features.OptimizelyDAM.REST.Media
{
    public abstract class BaseRestFile : BaseRestItem, IPublicDamUrl
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("file_size")]
        public double FileSize { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}