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

        private static List<Movie> allMoviesList = new List<Movie>();
        private static int currentHigherPage = 0;
        private static int totalPages = 0;
        private static int totalItems = 0;

        public MovieService(IGenreService genreService)
        {
            this.genreService = genreService;

            //Setting the API token in request header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("TMDB_API_TOKEN"));
        }

        //This function requests a page the latest movies from TMDB API and returns an array of Movie objects of them
        public async Task RequestLatestMovies(int? page)
        //public async Task<List<Movie>> RequestLatestMovies(int? page)
        {
            page ??= 1;

            //Invalid page verification
            if (page.Value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            //Page skip verification
            if(page.Value - currentHigherPage != 1)
            {
                throw new ArgumentException();
            }

            //Query for page
            string query = "?page=" + page.Value;

            //TMDB latest movies' API route path 
            string path = Environment.GetEnvironmentVariable("TMDB_API_ADDRESS") + "/movie/upcoming" + query;

            //Sending request to route and getting the response asynchronously
            HttpResponseMessage response = await httpClient.GetAsync(path);

            //Deserializing response in model object
            JsonModel jsonModel = await response.Content.ReadAsAsync<JsonModel>();

            //Populating the genres list in each Movie
            foreach (var movie in jsonModel.Results)
            {
                movie.Genres = await genreService.GetGenres(movie.Genre_ids);
            }

            //Updating number of total pages, total items and current page(if necessary)
            totalPages = jsonModel.Total_pages;
            totalItems = jsonModel.Total_results.Value;

            if(page.Value > currentHigherPage)
            {
                currentHigherPage = page.Value;
            }

            //Append results in already existing list
            allMoviesList.AddRange(jsonModel.Results);

            Console.WriteLine("Request TMDB API - Page " + page.Value);

            //return jsonModel.Results;
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
        public string MakeJsonWithAdditionalFields(List<Movie> moviesList, List<string> additionalParametersList, int page, int totalPages)
        {
            NullifyUnwantedAttributes(moviesList, additionalParametersList);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            //Creating JsonModel object for correct JSON formatting
            JsonModel model = new JsonModel(moviesList, page, totalPages);

            return JsonSerializer.Serialize(model, options);
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

        //Returns a movie list corresponding to the selected page with selected size
        public async Task<List<Movie>> GetMoviePage(int? page, int? pageSize)
        {
            //Bad parameter verification
            if (page <= 0 || pageSize <= 0)
            {
                return null;
            }

            //Calculating to see in which API page (which has size 20) the data window ends
            int endItem = page.Value * pageSize.Value;
            int endPage = (int)Math.Ceiling((float)endItem / 20);

            //Get movies equivalent to the page
            int startItem = (page.Value - 1) * pageSize.Value;

            int range = pageSize.Value;

            //If end page is greater that current requested higher page, request the next needed pages
            if(endPage > currentHigherPage)
            {
                for(int i = currentHigherPage + 1; i <= endPage; i++)
                {
                    //Total page overflow
                    if(totalPages != 0 && i > totalPages)
                    {
                        break;
                    }
                    await RequestLatestMovies(i);
                }
            }

            //Last page verification
            if (endPage >= totalPages)
            {
                endPage = totalPages;
                endItem = totalItems;
                range = endItem - startItem;
            }

            //Start overflow verification
            if (startItem > endItem)
            {
                return new List<Movie>();
            }

            else
            {
                List<Movie> pageMovies = allMoviesList.GetRange(startItem, range);

                //Deep cloning list for return without references
                return DeepClone(pageMovies);
            }
        }

        //Calculates and returns the total number of pages according to page size
        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((float)totalItems / pageSize);
        }

        //Deep clones a movie list
        public List<Movie> DeepClone(List<Movie> list)
        {
            return list.ConvertAll(movie => new Movie(movie.Title, movie.Genres, movie.Release_date, movie.Genre_ids, movie.Popularity, movie.Vote_count, movie.Video, movie.Poster_path, movie.Id, movie.Adult, movie.Backdrop_path, movie.Original_language, movie.Original_title, movie.Vote_average, movie.Overview));
        }
    }
}
