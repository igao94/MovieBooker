using Application.Showtimes.Commands.AddShowtime;
using Application.Showtimes.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ShowtimesController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ShowtimeDto>> AddShowTime(CreateShowtimeDto createShowTimeDto)
    {
        return HandleResult(await Mediator.Send(new AddShowtimeCommand(createShowTimeDto)));
    }
}
