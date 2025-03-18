using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IGenericRepository<Movie> _repository = unitOfWork.Repository<Movie>();

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Movie>>> GetMovies()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(string id)
    {
        var activity = await _repository.GetByIdAsync(id);

        if (activity is null) return NotFound();

        return activity;
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> AddMovie(Movie movie)
    {
        _repository.Add(movie);

        var result = await unitOfWork.CompleteAsync();

        return result ? CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie(string id, Movie movie)
    {
        if (!await _repository.ExsistsAsync(id)) return BadRequest("Movie doesn't exist.");

        _repository.Update(movie);

        var result = await unitOfWork.CompleteAsync();

        return result ? NoContent() : BadRequest("Problem updating movie");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(string id)
    {
        var movie = await _repository.GetByIdAsync(id);

        if (movie is null) return NotFound();

        _repository.Remove(movie);

        var result = await unitOfWork.CompleteAsync();

        return result ? NoContent() : BadRequest("Problem deleting movie.");
    }
}
