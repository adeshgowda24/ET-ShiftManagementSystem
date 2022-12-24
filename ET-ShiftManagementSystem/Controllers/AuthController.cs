using Azure.Core;
using Azure.Identity;
using ET_ShiftManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ShiftManagementServises.Servises;
using ShiftMgtDbContext.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ET_ShiftManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserCredential Users = new UserCredential();
        private readonly IConfiguration _configuration;

        private readonly ICredentialServices _credentialServices;
        public AuthController(IConfiguration configuration , ICredentialServices credentialServices )
        {
            _configuration = configuration;
            _credentialServices  = credentialServices;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserCredential>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] PasswordSalt);


            Users.UserName = request.UserName;
            Users.PasswordHash = passwordHash;
            Users.PasswordSalt = PasswordSalt;
            var cred = new UserCredential();
            {
                //Users.UserName = request.UserName;
                cred.PasswordHash = passwordHash;
                cred.PasswordSalt = PasswordSalt;

            
                cred.UserName = request.UserName;
                cred.Password = request.Password;
            }
            _credentialServices.addcredentil(cred);


           // return Ok(Users);
            return Ok(cred);

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            //var cred = new UserCredential();
            if (Users.UserName != request.UserName)
            {
                return BadRequest("user not exist"); 
            }

            if (VerifyPasswordHash(request.Password, Users.PasswordHash, Users.PasswordSalt))
            {
                return BadRequest("passworrd is invalid ");
            }

            string token = GenerateToken(Users);

            
            return Ok(token);
        }

        //private string CreateToken(UserCredential user)
        //{
        //    List<Claim> claims = new List<Claim>()
        //    { 
        //        new Claim(ClaimTypes.Name , user.UserName)
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configuration["Jwt:Issuer"]));

        //    var credentialDetails = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(_configuration["Jwt:Key"],
        //       claims : claims,
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentialDetails);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);  

        //    return jwt;
        //}

        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            // Get the JWT from the request header
            var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Invalidate the JWT by adding it to a list of invalidated tokens
            // You will need to store this list somewhere, such as in a database or cache
            //if (invalidatedTokens.Contains(jwt))
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    return;
            //}

            //invalidatedTokens.Add(jwt);

            return Ok();
        }

        private string GenerateToken(UserCredential user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                //new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        private void CreatePasswordHash(string password ,out byte[] passwordHash ,out  byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordHash = hmac.Key;
                passwordSalt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash , byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


    }
}
