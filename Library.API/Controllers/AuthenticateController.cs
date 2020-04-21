using Library.Application.Common.Models.Requests;
using Library.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public AuthenticateController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VBy0OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SBM=)"));

                var token = new JwtSecurityToken(
                    issuer: "http://localhost:51044",
                    audience: "http://localhost:51044",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    var newUser = new ApplicationUser { 
        //        UserName = model.Username, 
        //        SecurityStamp = Guid.NewGuid().ToString(), 
        //        Email = model.Email };

        //    var createUser = await _userManager.CreateAsync(newUser, model.Password);

        //    return Ok();
        //}
    }
}
