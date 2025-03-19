using Application.Movies.Commands.AddMovie;
using Application.Movies.Commands.DeleteMovie;
using Application.Movies.Commands.UpdateMovie;
using Application.Movies.DTOs;
using Application.Movies.Queries.GetAllMovies;
using Application.Movies.Queries.GetMovieById;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Movie>>> GetMovies()
    {
        return HandleResult(await Mediator.Send(new GetAllMoviesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(string id)
    {
        return HandleResult(await Mediator.Send(new GetMovieByIdQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> AddMovie(CreateMovieDto movieDto)
    {
        var result = await Mediator.Send(new AddMovieCommand(movieDto));

        return HandleCreatedResult<Movie>(nameof(GetById), new { id = result.Value?.Id }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie(string id, UpdateMovieDto movieDto)
    {
        movieDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateMovieCommand(movieDto)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteMovieCommand(id)));
    }
}
