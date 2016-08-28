using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoopClient.Models
{
    public class StreamModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("stream")]
        public string StreamUrl { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

    }
}
