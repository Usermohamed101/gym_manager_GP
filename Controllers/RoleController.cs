using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {


        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roleManager.Roles.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string role )
        {
            if (await _roleManager.RoleExistsAsync(role))
                return BadRequest("Role already exists.");

            var res = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!res.Succeeded) return BadRequest(res.Errors);

            return Ok("Role created.");
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null) return NotFound();

            var res = await _roleManager.DeleteAsync(role);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return NoContent();
        }
    }
}
