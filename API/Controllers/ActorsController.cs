using Application.Actors.Commands.AddActorToMovie;
using Application.Actors.Commands.CreateActor;
using Application.Actors.DTOs;
using Domain.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActorsController : BaseApiController
{
    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<ActorDto>> CreateActor(CreateActorDto createActorDto)
    {
        return HandleResult(await Mediator.Send(new CreateActorCommand(createActorDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("add-actor-to-movie/{movieId}/{actorId}")]
    public async Task<ActionResult> AddActorToMovie(string movieId, string actorId)
    {
        return HandleResult(await Mediator.Send(new AddActorToMovieCommand(movieId, actorId)));
    }
}
