using GymManagmentSystem.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {



            private readonly GymDb _context;

            public TrainerController(GymDb context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                return Ok(_context.Users.OfType<Trainer>().ToList());
            }

            [HttpGet("{id}")]
            public IActionResult Get(string id)
            {
                var trainer = _context.Users.OfType<Trainer>().FirstOrDefault(x => x.Id == id);
                if (trainer == null) return NotFound();
                return Ok(trainer);
            }

            [HttpPost]
            public IActionResult Create([FromBody] Trainer trainer)
            {
                _context.Users.Add(trainer);
                _context.SaveChanges();
                return Ok(trainer);
            }

            [HttpPost("{trainerId}/assign-member/{memberId}")]
            public IActionResult AssignMember(string trainerId, string memberId)
            {
                var trainer = _context.Users.OfType<Trainer>().FirstOrDefault(x => x.Id == trainerId);
                var member = _context.Users.OfType<Member>().FirstOrDefault(x => x.Id == memberId);

                if (trainer == null || member == null)
                    return NotFound();

                trainer.Members.Add(member);
                _context.SaveChanges();

                return Ok();
            }
        }



    
}
