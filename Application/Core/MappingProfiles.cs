using Application.Actors.DTOs;
using Application.Movies.DTOs;
using Application.Showtimes.ShowtimeDTOs;
using Application.Showtimes.ShowtimeSeatDTOs;
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

        CreateMap<UpdateActorDto, Actor>();

        CreateMap<MovieActor, ActorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActorId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Actor.FullName))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Actor.PictureUrl));

        CreateMap<Showtime, ShowtimeDto>()
            .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.ShowtimeSeats
                .Count(ss => !ss.IsReserved)));

        CreateMap<UpdateShowtimeDto, Showtime>();

        CreateMap<ShowtimeSeat, ShowtimeSeatDto>();
    }
}
