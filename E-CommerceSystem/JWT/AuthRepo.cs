using E_CommerceSystem.Data;
using E_CommerceSystem.DTOs.Auth;
using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceSystem.JWT
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext context;
        private readonly IConfiguration config;
        public AuthRepo(AppDbContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var existUser = await context.Users.FirstOrDefaultAsync(c => c.Email ==  loginDTO.Email);
            if (existUser == null)
            {
                return null;
            }
            if(existUser.Password == loginDTO.Password)
            {
                return GenerateToken(existUser);
            }
            return null;
        }

        private string GenerateToken(User user)
        {
            #region claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };
            #endregion

            #region key 

            var Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWT:Key"]));

            var signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            #endregion
            var TokenObj = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(config["JWT:ExpireInHours"]))
            );

            var Token = new JwtSecurityTokenHandler().WriteToken(TokenObj);
            return Token;
        }
        public async Task<bool> Register(User user)
        {
            var existUser = await context.Users.AnyAsync(c => c.Email == user.Email);
            if (existUser)
            {
                return false;
            } 
            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }
    }
}
