using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSithec.Data;
using TestSithec.Models;

namespace TestSithec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanController : ControllerBase
    {
        private readonly DBContext _context;

        public HumanController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Human
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Human>>> GetHuman()
        {
          if (_context.Human == null)
          {
              return NotFound();
          }
            return await _context.Human.ToListAsync();
        }

        [HttpGet]
        [Route("getMock")]
        public dynamic getHumans()
        {
            MockHumans MockData = new MockHumans();

            return MockData.getHumans();
        }

        // GET: api/Human/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Human>> GetHuman(int id)
        {
          if (_context.Human == null)
          {
              return NotFound();
          }
            var human = await _context.Human.FindAsync(id);

            if (human == null)
            {
                return NotFound();
            }

            return human;
        }

        // PUT: api/Human/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHuman(int id, Human human)
        {
            if (id != human.Id)
            {
                return BadRequest();
            }

            _context.Entry(human).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanExists(id))
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

        // POST: api/Human
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Human>> PostHuman(Human human)
        {
          if (_context.Human == null)
          {
              return Problem("Entity set 'DBContext.Human'  is null.");
          }
            _context.Human.Add(human);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHuman", new { id = human.Id }, human);
        }

        // DELETE: api/Human/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHuman(int id)
        {
            if (_context.Human == null)
            {
                return NotFound();
            }
            var human = await _context.Human.FindAsync(id);
            if (human == null)
            {
                return NotFound();
            }

            _context.Human.Remove(human);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanExists(int id)
        {
            return (_context.Human?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
