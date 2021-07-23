using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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

            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password)
        }
    }
}
