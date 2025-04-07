using Application.Core;
using MediatR;

namespace Application.Movies.Commands.DeleteMoviePoster;

public class DeleteMoviePosterCommand(string posterId) : IRequest<Result<Unit>>
{
    public string PosterId { get; set; } = posterId;
}
