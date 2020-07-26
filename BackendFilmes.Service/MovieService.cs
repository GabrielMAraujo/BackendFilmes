using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackendFilmes.Service.Interfaces;
using BackendFilmes.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BackendFilmes.Service
{
    public class MovieService : IMovieService
    {
        private HttpClient httpClient = new HttpClient();
        private IGenreService genreService;

        public MovieService(IGenreService genreService)
        {
            this.genreService = genreService;

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

            //Populating the genres list in each Movie
            foreach (var movie in jsonModel.Results)
            {
                movie.Genres = await genreService.GetGenres(movie.Genre_ids);
            }


            return jsonModel.Results;
        }

        //Parses a comma-separated string array of additional parameters to a list of strings
        public List<string> ParseAdditionalParams(string additionalParams)
        {
            if(additionalParams == null)
            {
                return null;
            }

            return additionalParams.Split(",").ToList();
        }


        //Makes JSON with default return fields and passed additional fields
        public string MakeJsonWithAdditionalFields(List<Movie> moviesList, List<string> additionalParametersList)
        {
            NullifyUnwantedAttributes(moviesList, additionalParametersList);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize<List<Movie>>(moviesList, options);
        }

        //Takes the attributes that are not default or additional and nulls them out (for JSON to ignore them)
        public void NullifyUnwantedAttributes(List<Movie> movieList, List<string> additionalParametersList)
        {
            //Using Reflection to get a list of all property names
            var properties = typeof(Movie).GetProperties().Select(p => p.Name).ToList();

            //Remove all default and additional parameters from list
            properties.Remove("Title");
            properties.Remove("Genres");
            properties.Remove("Release_date");

            if(additionalParametersList != null) {
                foreach(var parameter in additionalParametersList)
                {
                    properties.Remove(CapitalizeString(parameter));
                }
            }

            //Nullify all remaining parameters in all Movie objects
            foreach(Movie movie in movieList)
            {
                foreach (var prop in properties)
                {
                    var property = movie.GetType().GetProperty(prop);
                    property.SetValue(movie, null, null);
                }
            }
        }

        //Capitalizes first letter of string. Used to match lower-case query properties to model property names.
        public string CapitalizeString(string str)
        {
            return char.ToUpper(str.First()) + str.Substring(1).ToLower();
        }

    }
}
