using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Recipe.Models;

namespace JWTAuthenticationExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly recipesContext _context;

        private Helper _helper = new Helper();

        public LoginController(IConfiguration config, recipesContext context)
        {
            _context = context;
            _config = config;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        User AuthenticateUser(User loginCredentials)
        {
            var encryptedPassword = _context.Users.Where(x => x.Username == loginCredentials.Username).Select(x => x.UserPassword).SingleOrDefault();
            var decryptedPassword = _helper.DecryptCipherTextToPlainText(encryptedPassword);
            User user = _context.Users.Where(x => x.Username == loginCredentials.Username && decryptedPassword == loginCredentials.UserPassword).FirstOrDefault();
            if (user == null)
            {
                BadRequest();
            }
            return user;
        }
        string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim("Username", userInfo.Username.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}