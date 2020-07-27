using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BackendFilmes.Model;
using BackendFilmes.Service.Interfaces;

namespace BackendFilmes.Service
{
    public class JsonService : IJsonService
    {

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

            if (additionalParametersList != null)
            {
                foreach (var parameter in additionalParametersList)
                {
                    properties.Remove(CapitalizeString(parameter));
                }
            }

            //Nullify all remaining parameters in all Movie objects
            foreach (Movie movie in movieList)
            {
                foreach (var prop in properties)
                {
                    //Using Reflection to nullify the list's attributes
                    var property = movie.GetType().GetProperty(prop);
                    property.SetValue(movie, null, null);
                }
            }
        }

        //Capitalizes first letter of string. Used to match lower-case query properties to model property names.
        public string CapitalizeString(string str)
        {
            if (str == null)
                return null;

            if(str.Length < 1)
                return "";

            return char.ToUpper(str.First()) + str.Substring(1).ToLower();
        }
    }
}
