using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [Route("/login")] 
    public class LoginController : Controller
    {        
        [HttpPost]        
        public IActionResult Post([FromBody]Credentials credentials)
        {
            if ( credentials.Password=="demo")
                return new ObjectResult(GenerateToken(credentials.Username));
            return BadRequest();
        }

        private string GenerateToken(string username)
        {     
            var key = System.Text.Encoding.UTF8.GetBytes("gizli anahtar burada olacak");
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

           /* var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));
*/
            var token = new JwtSecurityToken(
                issuer: "AuthService",
                audience: "oguz",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}