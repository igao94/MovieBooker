using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        return await context.Movies.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(string id)
    {
        var activity = await context.Movies.FindAsync(id);

        if (activity is null) return NotFound();

        return activity;
    }
}
