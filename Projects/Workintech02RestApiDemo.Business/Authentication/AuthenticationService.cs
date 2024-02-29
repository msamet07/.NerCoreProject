using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Domain.Entities;
using Workintech02RestApiDemo.Domain.Helper;
using Workintech02RestApiDemo.Infrastructure;

namespace Workintech02RestApiDemo.Business.Authentication
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly Workintech02CodeFirstContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(Workintech02CodeFirstContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User Login(string username, string password)
        {

            var user = _context.Users
                    .Where(x => x.UserName == username)
                    .Include(x=>x.UserRoles)
                    .ThenInclude(x=>x.Role)
                    .FirstOrDefault();
            if (user == null)
                return null;

            var verifyPassword = Utils.CompareHashPassword(password,user.Password);
            if (!verifyPassword)
                return null;

            return user;
        }

        public string GenerateToken(User user)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");

            byte[] secretKeyByteArray = Encoding.UTF8.GetBytes(secretKey);

            var securityKey = new SymmetricSecurityKey(secretKeyByteArray);
            var credientials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

           
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var claims = new List<Claim>
               {  
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               };

            List<Claim> userRoles = user.UserRoles.Select(userRole => new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole.Role.Name)).ToList();

            claims.AddRange(userRoles);


            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = credientials
            };
            var securityToken = tokenHandler.CreateToken(token);
            var tokenString = tokenHandler.WriteToken(securityToken);
            return tokenString;
        }

        public User Register(User user)
        {
            var hashedPassword = Utils.HashPassword(user.Password);
            user.Password = hashedPassword;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
