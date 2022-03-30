using AcmeBackEnd.Data;
using AcmeBackEnd.DTO;
using AcmeBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AcmeBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto req)
        {
            createPassword(req.Password, out byte[] passHash, out byte[] passSalt);
            
            var user = new UserModel
            {
                UserName = req.UserName,
                PasswordHash = passHash,
                PasswordSalt = passSalt
            };
            _context.UserModel.Add(user);
            await _context.SaveChangesAsync();

            return Ok(createUserDto(user, ""));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserDto req)
        {
            foreach(var user in _context.UserModel)
            {
                if (user.UserName == req.UserName)
                {
                    bool check = CheckPassword(req.Password, user.PasswordHash, user.PasswordSalt);

                    if (!check)
                        return BadRequest("Wrong password");

                    string token = createToken(user);

                    return Ok(createUserDto(user, token));
                }
            }

            return NotFound();
        }

        private string createToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, signingCredentials: creds, expires: DateTime.Now.AddDays(1));

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void createPassword(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool CheckPassword(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new HMACSHA512(passSalt))
            {
                var passCompute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passCompute.SequenceEqual(passHash);
            }
        }

        private static UserDto createUserDto(UserModel user, string token)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = "",
                Token = token
            };
        }
    }
}
