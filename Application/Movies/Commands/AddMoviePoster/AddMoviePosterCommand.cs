using Application.Core;
using Application.Movies.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Movies.Commands.AddMoviePoster;

public class AddMoviePosterCommand : IRequest<Result<MoviePosterDto>>
{
    public required string Id { get; set; }
    public required IFormFile File {  get; set; }
}
