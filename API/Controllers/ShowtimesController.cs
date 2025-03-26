using Application.Showtimes.Commands.AddShowtime;
using Application.Showtimes.Commands.DeleteShowtime;
using Application.Showtimes.Commands.UpdateShowtime;
using Application.Showtimes.ShowtimeDTOs;
using Domain.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ShowtimesController : BaseApiController
{
    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<ShowtimeDto>> AddShowtime(CreateShowtimeDto createShowTimeDto)
    {
        return HandleResult(await Mediator.Send(new AddShowtimeCommand(createShowTimeDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateShowtime(string id, UpdateShowtimeDto updateShowtimeDto)
    {
        updateShowtimeDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateShowtimeCommand(updateShowtimeDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShowtime(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteShowtimeCommand(id)));
    }
}
