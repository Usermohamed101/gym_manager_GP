using GymManagmentSystem.DataBase;
using GymManagmentSystem.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        private string GenerateJwt(User user, IList<string> roles, out DateTime expiresAt)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiryMinutes = int.Parse(_config["Jwt:ExpiryMinutes"] ?? "60");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.Email ?? ""),
            new Claim("fullname", $"{user.FirstName} {user.SecName}")
        };

            foreach (var r in roles)
                claims.Add(new Claim(ClaimTypes.Role, r));

            expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.Email);
            if (exists != null) return BadRequest("Email already exists.");

            User user;

            // Create a specific subtype
            if (dto.Role == "Member")
                user = new Member { Email = dto.Email, UserName = dto.Email, FirstName = dto.FirstName, SecName = dto.LastName, EmailConfirmed = true };
            else if (dto.Role == "Trainer")
                user = new Trainer { Email = dto.Email, UserName = dto.Email, FirstName = dto.FirstName, SecName = dto.LastName, EmailConfirmed = true };
            else
                user = new User { Email = dto.Email, UserName = dto.Email, FirstName = dto.FirstName, SecName = dto.LastName ,EmailConfirmed=true};

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            // Assign role
            if (!await _userManager.IsInRoleAsync(user, dto.Role))
                await _userManager.AddToRoleAsync(user, dto.Role);

            // Generate JWT directly here
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwt(user, roles, out var expiresAt);

            return Ok(new AuthResponseDto
            {
                Token = token,
                ExpiresAt = expiresAt,
                UserId = user.Id,
                Roles = roles
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized("Invalid email or password.");

            var isValid = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!isValid.Succeeded) return Unauthorized("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwt(user, roles, out var expiresAt);

            return Ok(new AuthResponseDto
            {
                Token = token,
                ExpiresAt = expiresAt,
                UserId = user.Id,
                Roles = roles
            });
        }
    }




}

