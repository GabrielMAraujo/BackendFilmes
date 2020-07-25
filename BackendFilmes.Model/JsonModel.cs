using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BackendFilmes.Model
{
    public class JsonModel
    {
        [JsonProperty("results")]
        public List<Movie> Results { get; set; }
    }
}
