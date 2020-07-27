using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackendFilmes.Model;
using BackendFilmes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        //public ActionResult<List<Movie>> Get(string additionalParams)
        public ActionResult<string> Get(string additionalParams, int? page, int? pageSize)
        {
            List<string> paramsList = movieService.ParseAdditionalParams(additionalParams);

            //List<Movie> movies = movieService.RequestLatestMovies(page).Result;

            //Setting default page and page size values
            page ??= 1;
            pageSize ??= int.Parse(Environment.GetEnvironmentVariable("DEFAULT_PAGE_SIZE"));

            List<Movie> movies = movieService.GetMoviePage(page.Value, pageSize.Value).Result;

            if(movies == null)
            {
                return BadRequest();
            }

            string json = movieService.MakeJsonWithAdditionalFields(movies, paramsList, page.Value, movieService.GetTotalPages(pageSize.Value));

            return Content(json, "application/json");
        }

    }
}
