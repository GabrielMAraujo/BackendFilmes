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
        private IMovieService service;

        public MovieController(IMovieService service)
        {
            this.service = service;
        }

        // GET /movie
        [HttpGet]
        public Task<List<Movie>> Get()
        {
            return service.RequestLatestMovies();
        }

    }
}
