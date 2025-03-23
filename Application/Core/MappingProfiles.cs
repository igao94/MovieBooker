﻿using Application.Actors.DTOs;
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

        CreateMap<CreateActorDto, Actor>();

        CreateMap<Actor, ActorDto>();

        CreateMap<MovieActor, ActorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActorId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Actor.FullName))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Actor.PictureUrl));
    }
}
