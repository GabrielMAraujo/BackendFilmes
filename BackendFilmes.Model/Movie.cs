using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendFilmes.Model
{
    public class Movie
    {
        public Movie(string title, List<Genre> genres, string release_date, List<int> genre_ids, decimal? popularity, int? vote_count, bool? video, string poster_path, int? id, bool? adult, string backdrop_path, string original_language, string original_title, decimal? vote_average, string overview)
        {
            Title = title;
            Genres = genres;
            Release_date = release_date;
            Genre_ids = genre_ids;
            Popularity = popularity;
            Vote_count = vote_count;
            Video = video;
            Poster_path = poster_path;
            Id = id;
            Adult = adult;
            Backdrop_path = backdrop_path;
            Original_language = original_language;
            Original_title = original_title;
            Vote_average = vote_average;
            Overview = overview;
        }

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
    