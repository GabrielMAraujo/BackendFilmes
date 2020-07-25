using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackendFilmes.Service.Interfaces;
using BackendFilmes.Model;
using System.Collections.Generic;

namespace BackendFilmes.Service
{
    public class MovieService : IMovieService
    {
        private HttpClient httpClient = new HttpClient();

        public MovieService()
        {
            //Setting the API token in request header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("TMDB_API_TOKEN"));
        }

        //This function requests the latest movies from TMBD API and returns an array of Movie objects of them
        public async Task<List<Movie>> RequestLatestMovies()
        {
            //TMDB latest movies' API route path 
            string path = Environment.GetEnvironmentVariable("TMDB_API_ADDRESS") + "/movie/upcoming";

            //Sending request to route and getting the response asynchronously
            HttpResponseMessage response = await httpClient.GetAsync(path);

            //Deserializing response in model object
            JsonModel jsonModel = await response.Content.ReadAsAsync<JsonModel>();

            return jsonModel.Results;
        }

    }
}
