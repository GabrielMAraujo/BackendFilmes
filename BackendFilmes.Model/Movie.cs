using System;
using System.Collections.Generic;

namespace BackendFilmes.Model
{
    public class Movie
    {
        //public int Id { get; set; }
        //public bool Adult { get; set; }
        //public string Backdrop_path { get; set; }
        public string Title { get; set; }
        public List<int> Genre_ids { get; set; }
        public List<Genre> Genres { get; set; }
        public string Release_date { get; set; }
    }
}
    