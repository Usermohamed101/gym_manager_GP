using GymManagmentSystem.DataBase;
using GymManagmentSystem.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PaymentsController : ControllerBase
        {
            private readonly GymDb _context;
            private readonly UserManager<User> _userManager;

            public PaymentsController(GymDb context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

         
            [HttpPost]
            [Authorize(Roles = "Admin,Trainer")]
            public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
            {
                var member = await _context.Members.FindAsync(dto.MemberId);
                if (member == null)
                    return NotFound("Member not found.");

                var membership = await _context.Memberships.FindAsync(dto.MembershipId);
                if (membership == null)
                    return NotFound("Membership not found.");

                var payment = new Payment
                {
                    MemberId = dto.MemberId,
                    MembershipId = dto.MembershipId,
                    Amount = dto.Amount,
                    PaymentMethod = dto.Method,
                  
                };

                _context.Payments.Add(payment);

             

                await _context.SaveChangesAsync();

                return Ok(new { message = "Payment recorded & membership activated." });
            }

            [HttpGet]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> GetAll()
            {
                var list = await _context.Payments
                    .Include(p => p.Member)
                    .Include(p => p.Membership)
                    .ToListAsync();

                return Ok(list);
            }

           
            [HttpGet("member/{memberId}")]
            [Authorize]
            public async Task<IActionResult> GetMemberPayments(string memberId)
            {
             
                var currentUserId = User.FindFirst("id")?.Value;
                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin && currentUserId != memberId)
                    return Unauthorized("You cannot view other members’ payments.");

                var payments = await _context.Payments
                    .Where(p => p.MemberId == memberId)
                    .Include(p => p.Membership)
                    .ToListAsync();

                return Ok(payments);
            }

           
            [HttpGet("{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> GetById(int id)
            {
                var payment = await _context.Payments
                    .Include(p => p.Member)
                    .Include(p => p.Membership)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (payment == null)
                    return NotFound("Payment not found.");

                return Ok(payment);
            }
        }


    }
}
