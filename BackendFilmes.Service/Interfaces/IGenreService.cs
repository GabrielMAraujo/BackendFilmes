using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendFilmes.Model;

namespace BackendFilmes.Service.Interfaces
{
    public interface IGenreService
    {
        public Task<List<Genre>> GetGenres(List<int> genresIdList);
        public Task<List<Genre>> GetAllGenresList();
    }
}
