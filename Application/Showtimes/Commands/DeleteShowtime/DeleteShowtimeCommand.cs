using Application.Core;
using MediatR;

namespace Application.Showtimes.Commands.DeleteShowtime;

public class DeleteShowtimeCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
