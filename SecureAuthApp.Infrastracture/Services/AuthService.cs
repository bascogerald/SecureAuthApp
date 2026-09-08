using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecureAuthApp.Core.Entities;
using SecureAuthApp.Core.Interfaces;
using SecureAuthApp.Infrastracture.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecureAuthApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        // We added IConfiguration here so we can read our secret JWT key later
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            // 1. Check if the user exists in PostgreSQL
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return "User not found";

            // 2. Check if the password matches the scrambled hash
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid) return "Invalid password";

            // 3. The password is correct! Let's create the VIP Wristband (JWT)
            return GenerateJwtToken(user);
        }

        // A private helper method to build the actual token string
        private string GenerateJwtToken(User user)
        {
            // Read the secret key we will create in the next step
            var keyString = _configuration["JwtSettings:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));

            // Add the user's ID and Role to the wristband
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2), // Wristband expires in 2 hours
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}