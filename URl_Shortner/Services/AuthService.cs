using URl_Shortner.DTOs;
using URl_Shortner.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using URl_Shortner.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace URl_Shortner.Services
{
    public class AuthService: IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            
        }
        public async Task<string> Login(LoginRequest logged)
        {
            var loggedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == logged.Email);
            if (loggedUser == null)
            {
                return "NO user FOund";
            }
            var passwordValid = BCrypt.Net.BCrypt.Verify(logged.Password, loggedUser.PasswordHash);
            if (passwordValid)
            {
                return "this will become the JWT soon";
            }
           
            return "Invalid email or password";
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
       
            };
            //Stamping Tool
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Building the Card
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires : DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

                
               
        }
        public async Task<string> Register(RegisterRequest registered)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == registered.Email);
            if (userExists)
            {
                return "User already exists" + userExists;
            }
            else
            {
                var userPassword = BCrypt.Net.BCrypt.HashPassword(registered.Password);
                User newUser = new User { Email = registered.Email, PasswordHash =userPassword };
                _context.Users.Add(newUser);
                var addingUser = await _context.SaveChangesAsync();
                return $"YOu are Now registered {newUser.Email}";
            }
            
            
            
        }
    }
}
