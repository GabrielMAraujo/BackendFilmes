using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendFilmes.Model
{
    public class Movie
    {
        //Default return parameters
        public string Title { get; set; }
        public List<Genre> Genres { get; set; }
        public string Release_date { get; set; }

        [JsonIgnore]
        public List<int> Genre_ids { get; set; }

        //Additional return parameters
        #nullable enable
        public decimal? Popularity { get; set; }
        public int? Vote_count { get; set; }
        public bool? Video { get; set; }
        public string? Poster_path { get; set; }
        public int? Id { get; set; }
        public bool? Adult { get; set; }
        public string? Backdrop_path { get; set; }
        public string? Original_language { get; set; }
        public string? Original_title { get; set; }
        public decimal? Vote_average { get; set; }
        public string? Overview { get; set; }
    }
}
    