using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthProject.Auth.Common;
using AuthProject.AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthProject.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;

        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }
        private List<Account> Accounts => new List<Account>
        {
            new Account()
            {
                Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"),
                Email = "kolya@gmail.com",
                Password = "kolya",
                Roles = new Role[] {Role.User}
            },
            new Account()
            {
                Id = Guid.Parse("e2372dc9-a849-4f3c-9004-df8fc921c13a"),
                Email = "petro@gmail.com",
                Password = "petro",
                Roles = new Role[] {Role.User}
            },
            new Account()
            {
                Id = Guid.Parse("e2373dc9-a849-4f3c-9004-df8fc921c13a"),
                Email = "bohdan@gmail.com",
                Password = "bohdan",
                Roles = new Role[] {Role.Admin}
            }
        };
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody]Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);
            if (user != null)
            {
                var token = GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            foreach(var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issure,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
