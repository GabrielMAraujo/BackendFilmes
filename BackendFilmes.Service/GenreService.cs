using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackendFilmes.Model;
using BackendFilmes.Service.Interfaces;

namespace BackendFilmes.Service
{
    public class GenreService : IGenreService
    {
        private HttpClient httpClient = new HttpClient();
        private List<Genre> genreList;

        public GenreService()
        {
            //Setting the API token in request header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("TMDB_API_TOKEN"));
        }

        //Returns a genre list from a genre ID list
        public async Task<List<Genre>> GetGenres(List<int> genresIdList)
        {
            if (genresIdList == null)
                return null;

            //Guarantees that genreList is updated
            genreList = await GetAllGenresList();

            //Gets filtered list with only genres that are in the ID list
            List<Genre> filteredList = genreList.Where(g => genresIdList.Contains(g.Id)).ToList();

            return filteredList;
        }

        //Returns all movie genres from TMDB API. If it was already requested, return previously requested genres list.
        public async Task<List<Genre>> GetAllGenresList()
        {
            if (genreList == null)
            {
                //TMDB movie genre list API route path 
                string path = Environment.GetEnvironmentVariable("TMDB_API_ADDRESS") + "/genre/movie/list";

                //Sending request to route and getting the response asynchronously
                HttpResponseMessage response = await httpClient.GetAsync(path);

                //Deserializing response in model object
                GenreJsonModel genreJsonModel = await response.Content.ReadAsAsync<GenreJsonModel>();

                return genreJsonModel.Genres;
            }

            else
            {
                return genreList;
            }
        }
    }
}
