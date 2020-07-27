using System;
using System.Collections.Generic;
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

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        // GET /movie
        [HttpGet]
        public ActionResult<string> Get(string additionalParams, int? page, int? pageSize)
        {
            List<string> paramsList = movieService.ParseAdditionalParams(additionalParams);

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
