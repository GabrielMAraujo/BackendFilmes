using System;
using System.Collections.Generic;
using BackendFilmes.Model;
using BackendFilmes.Service;
using Xunit;

namespace BackendFilmes.Test.Services
{
    public class JsonServiceTest
    {
        private JsonService jsonService = new JsonService();

        [Theory]
        [InlineData("prop", "Prop")]
        [InlineData("1234", "1234")]
        [InlineData("1prop", "1prop")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("a", "A")]
        public void CapitalizeLetterTest(string originalString, string expected)
        {
            string result = jsonService.CapitalizeString(originalString);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NullifyCorrectAttributesTest()
        {
            List<Movie> movieList = new List<Movie>();

            List<Genre> listGenre = new List<Genre>();
            listGenre.Add(new Genre(99, "TestGenre"));

            movieList.Add(new Movie("Test movie", listGenre, "2000-01-01", new List<int> { 99 }, (decimal)6.33, null, null, null, null, null, null, null, null, null, "overview"));

            jsonService.NullifyUnwantedAttributes(movieList, new List<string> { "overview" });

            Assert.Equal("Test movie", movieList[0].Title);
            Assert.Null(movieList[0].Popularity);
            Assert.Equal("overview", movieList[0].Overview);
        }
    }
}
