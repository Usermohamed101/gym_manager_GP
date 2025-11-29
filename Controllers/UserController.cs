using GymManagmentSystem.DataBase;
using GymManagmentSystem.Dtos;
using GymManagmentSystem.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepo userRepo;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IUserRepo userRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = userRepo.getAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user =  userRepo.getById(id);
            if (user == null) return NotFound();
            var roles = await userRepo.GetUserRolesAsync(id);
            return Ok(new { user.Id, user.UserName, user.Email, user.PhoneNumber, Roles = roles });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterDto dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.Email);
            if (exists != null) return BadRequest("Email already exists.");

            User user;
            // create specific subtype if needed
            if (dto.Role.Equals("Trainer", StringComparison.OrdinalIgnoreCase))
                user = new Trainer { UserName = dto.Email, Email = dto.Email, FirstName = dto.FirstName, SecName = dto.LastName };
            else if (dto.Role.Equals("Member", StringComparison.OrdinalIgnoreCase))
                user = new Member { UserName = dto.Email, Email = dto.Email, FirstName = dto.FirstName, SecName = dto.LastName };
            else
                user = new User { UserName = dto.Email, Email = dto.Email, FirstName = dto.FirstName, SecName = dto.LastName };

            var create = await _userManager.CreateAsync(user, dto.Password);
            if (!create.Succeeded) return BadRequest(create.Errors);

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));

            await _userManager.AddToRoleAsync(user, dto.Role);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { user.Id, user.Email });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.SecName = dto.LastName;
            if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;
            if (dto.ProfilePictureUrl != null) user.ProfileImageUrl = dto.ProfilePictureUrl;

            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var res = await _userManager.DeleteAsync(user);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return NoContent();
        }

        [HttpPost("{id}/roles")]
        public async Task<IActionResult> AssignRole(string id, string Role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (!await _roleManager.RoleExistsAsync(Role))
                await _roleManager.CreateAsync(new IdentityRole(Role));

            var res = await _userManager.AddToRoleAsync(user, Role);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return Ok();
        }

        [HttpDelete("{id}/roles")]
        public async Task<IActionResult> RemoveRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var res = await _userManager.RemoveFromRoleAsync(user, role);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return Ok();

        }











    }
}
