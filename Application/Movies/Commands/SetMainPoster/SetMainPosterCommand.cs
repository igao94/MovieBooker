using Application.Core;
using MediatR;

namespace Application.Movies.Commands.SetMainPoster;

public class SetMainPosterCommand(string movieId, string posterId) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
    public string PosterId { get; set; } = posterId;
}
