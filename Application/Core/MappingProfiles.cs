using Application.Movies.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Movie, MovieDto>();

        CreateMap<CreateMovieDto, Movie>();

        CreateMap<UpdateMovieDto, Movie>();
    }
}
