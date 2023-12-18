using Azure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetManager.Data;
using NetManager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NetManager.Helpers;

namespace NetManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginsController : Controller
    {
        private readonly NetManagerContext _context;

        public LoginsController(NetManagerContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([Bind("Email,Password")] Login login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == login.Email);
            if (user == null)
            {
                return NotFound();
            }

            if (Helpers.PasswordHasher.VerifyPassword(login.Password, user.Password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:4200",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
