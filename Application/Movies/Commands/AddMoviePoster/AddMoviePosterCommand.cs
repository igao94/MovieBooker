using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Movies.Commands.AddMoviePoster;

public class AddMoviePosterCommand : IRequest<Result<Unit>>
{
    public required string Id { get; set; }
    public required IFormFile File {  get; set; }
}
