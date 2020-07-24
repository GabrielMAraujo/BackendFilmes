using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BackendFilmes.Service.Interfaces;

namespace BackendFilmes.Service
{
    public class MovieService : IMovieService
    {
        HttpClient httpClient = new HttpClient();

        public MovieService()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("TMDB_API_TOKEN"));
        }

        public async void Request()
        {
            Console.WriteLine("test");

            //TMDB latest movies' API route path 
            string path = Environment.GetEnvironmentVariable("TMDB_API_ADDRESS") + "/movie/latest";

            //Sending request to route and getting the response asynchronously
            HttpResponseMessage response = await httpClient.GetAsync(path);

            string text = await response.Content.ReadAsStringAsync();
            Console.WriteLine(text) ;
        }
    }
}
