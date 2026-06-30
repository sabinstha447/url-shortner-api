using URl_Shortner.DTOs;
using URl_Shortner.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using URl_Shortner.Models;
using System.ComponentModel.DataAnnotations;

namespace URl_Shortner.Services
{
    public class AuthService: IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
            
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
                return "Invalid email or password";
            }
           
            return "No User Found";
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
