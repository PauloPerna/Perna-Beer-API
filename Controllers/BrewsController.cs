using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class BrewsController : ControllerBase
    {
        private readonly DBContext _context;

        public BrewsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Brews
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Brew>>> GetBrews(decimal? abvMin, decimal? abvMax, decimal? ibuMin, decimal? ibuMax)
        {
            var brews = await _context.Brews.ToListAsync();

            if(abvMin.HasValue)
            {
                brews = brews.Where(b => b.Abv >= abvMin).ToList();
            }
            if(abvMax.HasValue)
            {
                brews = brews.Where(b => b.Abv <= abvMax).ToList();
            }
            if(ibuMin.HasValue)
            {
                brews = brews.Where(b => b.Ibu >= ibuMin).ToList();
            }
            if(ibuMax.HasValue)
            {
                brews = brews.Where(b => b.Ibu <= ibuMax).ToList();
            }

            return brews;
        }

        // GET: api/Brews/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Brew>> GetBrew(int id)
        {
            if (!BrewExists(id))
            {
                return NotFound();
            }

            var brew = await _context.Brews.FindAsync(id);
            return brew;
        }

        // GET: api/Brews/Search
        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Brew>>> SearchBrews(string q)
        {
            var brews = await _context.Brews
                .Where(b => b.Name.Contains(q) || b.Style.Contains(q) || b.Description.Contains(q))
                .ToListAsync();
            
            if(brews == null)
            {
                return NotFound();
            }

            return Ok(brews);
        }

        // POST: api/Brews/Create
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Brew>> PostBrew(Brew brew)
        {
            brew.Id = 0;

            _context.Brews.Add(brew);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrew), new { id = brew.Id }, brew);
        }

        // POST: api/Brews/CreateMultiple
        [HttpPost]
        [Route("CreateMultiple")]
        public async Task<ActionResult<IEnumerable<Brew>>> PostBrews(List<Brew> brews)
        {
            brews.ForEach(b => b.Id = 0);

            _context.Brews.AddRange(brews);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrews), new { }, brews);
        }

        // PUT: api/Brews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrew(int id, Brew brew)
        {
            brew.Id = id;

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
            if (!BrewExists(id))
            {
                return NotFound();
            }

            var brew = await _context.Brews.FindAsync(id);
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
