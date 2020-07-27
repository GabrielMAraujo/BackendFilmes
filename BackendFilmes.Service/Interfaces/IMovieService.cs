using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendFilmes.Model;

namespace BackendFilmes.Service.Interfaces
{
    public interface IMovieService
    {
        public Task<List<Movie>> GetMoviePage(int? page, int? pageSize);
        public List<string> ParseAdditionalParams(string additionalParams);
        public int GetTotalPages(int pageSize);
    }
}
