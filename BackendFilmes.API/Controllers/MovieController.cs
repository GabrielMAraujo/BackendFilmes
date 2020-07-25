using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackendFilmes.Model;
using BackendFilmes.Model.DTOs;
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
        private IMapper mapper;

        public MovieController(IMovieService movieService, IGenreService genreService, IMapper mapper)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.mapper = mapper;
        }

        // GET /movie
        [HttpGet]
        public ActionResult<List<MovieDTO>> Get()
        {
            return mapper.Map<List<MovieDTO>>(movieService.RequestLatestMovies().Result);
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
