using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendFilmes.Model;
using BackendFilmes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendFilmes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private IMovieService movieService;
        private IGenreService genreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
        }

        // GET /movie
        [HttpGet]
        public Task<List<Movie>> Get()
        {
            return movieService.RequestLatestMovies();
        }

        // GET /movie/genres
        [HttpGet("genres")]
        public Task<List<Genre>> GetGenres()
        {
            //return genreService.GetAllGenresList();
            return genreService.GetGenres(new List<int> { 28, 35, 878, 10751 });
        }
    }
}
