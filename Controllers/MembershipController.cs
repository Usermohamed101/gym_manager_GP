using GymManagmentSystem.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly GymDb _context;

        public MembershipController(GymDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Memberships.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var m = _context.Memberships.Find(id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Membership m)
        {
            _context.Memberships.Add(m);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = m.Id }, m);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Membership m)
        {
            var entity = _context.Memberships.Find(id);
            if (entity == null) return NotFound();

            entity.Name = m.Name;
            entity.Price = m.Price;
            entity.DurationInDays = m.DurationInDays;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _context.Memberships.Find(id);
            if (entity == null) return NotFound();

            _context.Memberships.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
