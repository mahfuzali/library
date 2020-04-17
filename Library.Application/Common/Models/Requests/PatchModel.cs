using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Common.Models.Requests
{
    public class PatchModel
    {
        [JsonProperty("op")]
        public string Op { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
