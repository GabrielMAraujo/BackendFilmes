using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendFilmes.Model;

namespace BackendFilmes.Service.Interfaces
{
    public interface IMovieService
    {
        public Task<List<Movie>> RequestLatestMovies();
        public List<string> GetGenres();
    }
}
