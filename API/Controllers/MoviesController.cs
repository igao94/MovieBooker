using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Movie>>> GetMovies()
    {
        return Ok(await unitOfWork.Repository<Movie>().GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(string id)
    {
        var activity = await unitOfWork.Repository<Movie>().GetByIdAsync(id);

        if (activity is null) return NotFound();

        return activity;
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> AddMovie(Movie movie)
    {
        unitOfWork.Repository<Movie>().Add(movie);

        var result = await unitOfWork.CompleteAsync();

        return result ? CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie(string id, Movie movie)
    {
        unitOfWork.Repository<Movie>().Update(movie);

        var result = await unitOfWork.CompleteAsync();

        return result ? NoContent() : BadRequest("Problem updating movie");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(string id)
    {
        var movie = await unitOfWork.Repository<Movie>().GetByIdAsync(id);

        if (movie is null) return NotFound();

        unitOfWork.Repository<Movie>().Remove(movie);

        var result = await unitOfWork.CompleteAsync();

        return result ? NoContent() : BadRequest("Problem deleting movie.");
    }
}
