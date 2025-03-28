﻿using Application.Actors.DTOs;
using Application.Core;
using MediatR;

namespace Application.Actors.Queries.GetActorById;

public class GetActorByIdQuery(string id) : IRequest<Result<ActorDto>>
{
    public string Id { get; set; } = id;
}
