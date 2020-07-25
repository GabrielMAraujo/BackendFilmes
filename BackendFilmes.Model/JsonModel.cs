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

    public class GenreJsonModel
    {
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
    }
}
