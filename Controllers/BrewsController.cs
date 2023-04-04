using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewsController : ControllerBase
    {
        private readonly DBContext _context;

        public BrewsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Brews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brew>>> GetBrews()
        {
            return await _context.Brews.ToListAsync();
        }

        // GET: api/Brews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brew>> GetBrew(int id)
        {
            var brew = await _context.Brews.FindAsync(id);

            if (brew == null)
            {
                return NotFound();
            }

            return brew;
        }

        // POST: api/Brews/Create
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Brew>> PostBrew(Brew brew)
        {
            var existingId = await _context.Brews.Where(b => b.Id == brew.Id).Select(b => b.Id).ToListAsync();
            if (existingId.Any())
            {
                return BadRequest($"The following Brew ID already exist: {string.Join(",", existingId)}");
            }
            _context.Brews.Add(brew);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrew), new { id = brew.Id }, brew);
        }

        // POST: api/Brews/CreateMultiple
        [HttpPost]
        [Route("CreateMultiple")]
        public async Task<ActionResult<IEnumerable<Brew>>> PostBrews(List<Brew> brews)
        {
            var repeatedIds = brews.GroupBy(b => b.Id)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();
            if (repeatedIds.Any())
            {
                return BadRequest($"The following Brew IDs are repeated in your request: {string.Join(",", repeatedIds)}");
            }

            var existingIds = await _context.Brews.Where(b => brews.Select(nb => nb.Id).Contains(b.Id)).Select(b => b.Id).ToListAsync();
            if (existingIds.Any())
            {
                return BadRequest($"The following Brew IDs already exist: {string.Join(",", existingIds)}");
            }

            _context.Brews.AddRange(brews);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrews), new { }, brews);
        }

        // PUT: api/Brews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrew(int id, Brew brew)
        {
            if (id != brew.Id)
            {
                return BadRequest();
            }

            _context.Entry(brew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrewExists(id))
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

        // DELETE: api/Brews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrew(int id)
        {
            var brew = await _context.Brews.FindAsync(id);
            if (brew == null)
            {
                return NotFound();
            }

            _context.Brews.Remove(brew);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrewExists(int id)
        {
            return _context.Brews.Any(e => e.Id == id);
        }
    }
}
