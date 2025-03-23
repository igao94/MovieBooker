using Application.Actors.Commands.AddActorToMovie;
using Application.Actors.Commands.CreateActor;
using Application.Actors.DTOs;
using Application.Actors.Queries;
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
        var result = await Mediator.Send(new CreateActorCommand(createActorDto));

        return HandleCreatedResult<ActorDto>(nameof(GetActorById), new { id = result.Value?.Id }, result.Value);
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("add-actor-to-movie/{movieId}/{actorId}")]
    public async Task<ActionResult> AddActorToMovie(string movieId, string actorId)
    {
        return HandleResult(await Mediator.Send(new AddActorToMovieCommand(movieId, actorId)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> GetActorById(string id)
    {
        return HandleResult(await Mediator.Send(new GetActorByIdQuery(id)));
    }
}
