using GymManagmentSystem.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {



            private readonly GymDb _context;
            private readonly UserManager<User> _userManager;

            public MemberController(GymDb context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var members = _context.Users.OfType<Member>().ToList();
                return Ok(members);
            }

            [HttpGet("{id}")]
            public IActionResult Get(string id)
            {
                var m = _context.Users.OfType<Member>().FirstOrDefault(x => x.Id == id);
                if (m == null) return NotFound();
                return Ok(m);
            }

            [HttpPost("{memberId}/assign-membership/{membershipId}")]
            public IActionResult AssignMembership(string memberId, int membershipId)
            {
                var member = _context.Users.OfType<Member>().FirstOrDefault(x => x.Id == memberId);
                var membership = _context.Memberships.Find(membershipId);

                if (member == null || membership == null)
                    return NotFound();

                var mm = new MemberMembership
                {
                    MemberId = memberId,
                    MembershipId = membershipId,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(membership.DurationInDays)
                };

                _context.MemberMemberships.Add(mm);
                _context.SaveChanges();
                return Ok(mm);
            }
        }



}
