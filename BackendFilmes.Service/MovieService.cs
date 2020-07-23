using System;
namespace BackendFilmes.Service
{
    public class MovieService
    {
        public void Request()
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("TMDB_API_TOKEN"));
        }
    }
}
