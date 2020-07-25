using System;
using AutoMapper;
using BackendFilmes.Model;
using BackendFilmes.Model.DTOs;

namespace BackendFilmes.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }
    }
}
