using Application.Movies.Commands.AddMovie;
using Application.Movies.Commands.DeleteMovie;
using Application.Movies.Commands.UpdateMovie;
using Application.Movies.DTOs;
using Application.Movies.Queries.GetAllMovies;
using Application.Movies.Queries.GetMovieById;
using Domain.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MovieDto>>> GetMovies()
    {
        return HandleResult(await Mediator.Send(new GetAllMoviesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovieById(string id)
    {
        return HandleResult(await Mediator.Send(new GetMovieByIdQuery(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<MovieDto>> AddMovie(CreateMovieDto movieDto)
    {
        var result = await Mediator.Send(new AddMovieCommand(movieDto));

        return HandleCreatedResult<MovieDto>(nameof(GetMovieById), new { id = result.Value?.Id }, result.Value);
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie(string id, UpdateMovieDto movieDto)
    {
        movieDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateMovieCommand(movieDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteMovieCommand(id)));
    }
}
