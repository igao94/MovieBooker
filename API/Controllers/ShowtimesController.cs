using Application.Showtimes.Commands.AddShowtime;
using Application.Showtimes.Commands.DeleteShowtime;
using Application.Showtimes.Commands.ReserveSeat;
using Application.Showtimes.Queries.GetAllShowtimes;
using Application.Showtimes.Queries.GetAvailableSeats;
using Application.Showtimes.Queries.GetShowtimeById;
using Application.Showtimes.ShowtimeDTOs;
using Application.Showtimes.ShowtimeSeatDTOs;
using Domain.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ShowtimesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ShowtimeDto>> GetAllShowtimes()
    {
        return HandleResult(await Mediator.Send(new GetAllShowtimesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShowtimeDto>> GetShowtimeById(string id)
    {
        return HandleResult(await Mediator.Send(new GetShowtimeByIdQuery(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<ShowtimeDto>> AddShowtime(CreateShowtimeDto createShowTimeDto)
    {
        var result = await Mediator.Send(new AddShowtimeCommand(createShowTimeDto));

        return HandleCreatedResult(result,
            nameof(GetShowtimeById),
            new { id = result.Value?.Id },
            result.Value);
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShowtime(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteShowtimeCommand(id)));
    }

    [HttpPost("reserve-seat")]
    public async Task<ActionResult> ReserveSeat(ReserveSeatCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpGet("available-seats")]
    public async Task<ActionResult<IReadOnlyList<SeatDto>>> GetAvaiableSeats(GetAvailableSeatsQuery query)
    {
        return HandleResult(await Mediator.Send(query));
    }
}
