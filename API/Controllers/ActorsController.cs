﻿using Application.Actors.Commands.AddActorToMovie;
using Application.Actors.Commands.CreateActor;
using Application.Actors.DTOs;
using Application.Actors.Queries.GetActorById;
using Application.Actors.Queries.GetAllActors;
using Domain.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Specifications.ActorsSpecification;

namespace API.Controllers;

public class ActorsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ActorDto>>> GetActors([FromQuery] ActorSpecParams specParams)
    {
        return HandleResult(await Mediator.Send(new GetAllActorsQuery(specParams)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> GetActorById(string id)
    {
        return HandleResult(await Mediator.Send(new GetActorByIdQuery(id)));
    }

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
}
