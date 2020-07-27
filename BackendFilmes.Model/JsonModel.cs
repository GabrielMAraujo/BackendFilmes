using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BackendFilmes.Model
{
    public class JsonModel
    {

        public JsonModel(List<Movie> results, int page, int total_pages)
        {
            Results = results;
            Page = page;
            Total_pages = total_pages;
        }

        [JsonProperty("results")]
        public List<Movie> Results { get; set; }

        public int Page { get; set; }
        public int Total_pages { get; set; }
        #nullable enable
        public int? Total_results { get; set; }
        #nullable disable
    }

    public class GenreJsonModel
    {
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
    }
}
