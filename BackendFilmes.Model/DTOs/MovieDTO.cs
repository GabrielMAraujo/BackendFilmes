using System;
using System.Collections.Generic;

namespace BackendFilmes.Model.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public List<Genre> Genres { get; set; }
        public string Release_date { get; set; }
    }
}
