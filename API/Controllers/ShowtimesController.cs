using Application.Showtimes.Commands.AddShowtime;
using Application.Showtimes.Commands.UpdateShowtime;
using Application.Showtimes.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ShowtimesController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ShowtimeDto>> AddShowtime(CreateShowtimeDto createShowTimeDto)
    {
        return HandleResult(await Mediator.Send(new AddShowtimeCommand(createShowTimeDto)));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateShowtime(string id, UpdateShowtimeDto updateShowtimeDto)
    {
        updateShowtimeDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateShowtimeCommand(updateShowtimeDto)));
    }
}
