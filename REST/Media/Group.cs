﻿using Newtonsoft.Json;

namespace Foundation.Features.OptimizelyDAM.REST.Media
{
    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}