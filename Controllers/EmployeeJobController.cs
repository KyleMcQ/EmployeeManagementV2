using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.DTOs;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeJobController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeJobController(EmployeeContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeJobDto>>> GetMovies()
        {
          if (_context.employeeJobs == null)
          {
              return NotFound();
          }
            return await _context.employeeJobs.Select(t =>
                new EmployeeJobDto()
                {
                    Id = t.Id,
                    JobTitle = t.JobTitle,
                    Description = t.Description,
                    EmployeeID = t.EmployeeID
                }
             ).ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.EmployeeJob>> GetMovie(Guid id)
        {
          if (_context.employeeJobs == null)
          {
              return NotFound();
          }
            var movie = await _context.employeeJobs.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, Models.EmployeeJob movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.EmployeeJob>> PostMovie(Models.EmployeeJob movie)
        {
          if (_context.employeeJobs == null)
          {
              return Problem("Entity set 'MoviesContext.Movies'  is null.");
          }
            _context.employeeJobs.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            if (_context.employeeJobs == null)
            {
                return NotFound();
            }
            var movie = await _context.employeeJobs.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.employeeJobs.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(Guid id)
        {
            return (_context.employeeJobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
