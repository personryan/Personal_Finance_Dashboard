using Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;
using DotNetEnv;
using System.Security.Cryptography;

namespace Backend.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(User user, IConfiguration config)
        {
            Env.Load();
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new Exception("JWT_SECRET environment variable is missing!");
            }
            //var jwtSettings = config.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["JwtHelper:ExpireMinutes"])),
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public static string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(RandomNumber);
                return Convert.ToBase64String(RandomNumber);
            }
        }
           
    }
    
}
