using Lab7.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DbContextApp _dbContext;
        public UserController(IConfiguration config, DbContextApp dbContextApp)
        {
            _config = config;
            _dbContext = dbContextApp;
        }
        [HttpPost]
        public async Task<object> Login(UserDto input)
        {
            var user = _dbContext.Account.Where(x => x.UserName == input.UserName 
                                                  && x.Password == input.Password)
                                         .FirstOrDefault();
            if(user == null)
            {
                throw new Exception("Account not found");
            }
            return GenerateToken(user);
        }
        private object GenerateToken(Account account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(_config["jwt:Secretkey"]);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",account.Id.ToString()),
                    new Claim(ClaimTypes.Name,account.UserName),
                    new Claim("TokenId",Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyByte)
                , SecurityAlgorithms.HmacSha256)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
