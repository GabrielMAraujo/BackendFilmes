using System;
using System.Collections;
using System.Collections.Generic;
using BackendFilmes.Model;
using Xunit;
using BackendFilmes.Service;
using System.Threading.Tasks;

namespace BackendFilmes.Test.Services
{
    //Test data classes
    public class GenreServiceTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<int> { 28 }, new List<Genre> { new Genre(28, "Action") } };
            yield return new object[] { new List<int>(), new List<Genre>() };
            yield return new object[] { new List<int> { 12, 10751, 14 }, new List<Genre> { new Genre(12, "Adventure"), new Genre(10751, "Family"), new Genre(14, "Fantasy") } };
            yield return new object[] { new List<int> { 9648, -1 }, new List<Genre> { new Genre(9648, "Mystery") } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    public class GenreServiceTest
    {
        private GenreService genreService = new GenreService();


        [Fact]
        public void GetGenresNullTest()
        {
            var result = genreService.GetGenres(null).Result;

            Assert.Null(result);
        }

        [Theory]
        [ClassData(typeof(GenreServiceTestData))]
        public async Task GetGenresValueTest(List<int> genreIdList, List<Genre> expectedGenres)
        {
            var result = await genreService.GetGenres(genreIdList);

            Assert.NotNull(result);

            for(int i = 0; i < expectedGenres.Count; i++)
            {
                var item = result.Find(g => g.Name == expectedGenres[i].Name && g.Id == expectedGenres[i].Id);
                Assert.NotNull(item);
            }
        }
    }
}
