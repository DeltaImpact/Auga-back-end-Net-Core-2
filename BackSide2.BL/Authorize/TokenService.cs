using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auga.BL.Extensions;
using Auga.BL.Models.AuthorizeDto;
using Auga.DAO.Entities;
using Auga.DAO.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auga.BL.Authorize
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;

        public TokenService(
            IConfiguration configuration,
            IRepository<User> userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public async Task<LoggedDto> RegisterAsync(RegisterDto model)
        {
            var person =
                await (await _userRepository.GetAllAsync(d => d.Email == model.Email || d.UserName == model.Username))
                    .FirstOrDefaultAsync();

            if (person != null)
            {
                if (person.UserName == model.Username && person.Email == model.Email)
                    throw new ArgumentException("Email and username already taken.");

                if (person.UserName == model.Username) throw new ArgumentException("Username already taken.");

                if (person.Email == model.Email) throw new ArgumentException("Email already taken.");
            }

            var systemUser = await (await _userRepository.GetAllAsync(d => d.UserName == "system"))
                .FirstOrDefaultAsync();
            if (systemUser == null)
                throw new ApplicationException("SystemUserNotFound");

            var newUser = await _userRepository.InsertAsync(model.ToPerson(systemUser));

            return newUser.ToLoggedDto(GenerateJwtToken(newUser));
        }

        public async Task<LoggedDto> LoginAsync(
            LoginDto model
        )
        {
            var person =
                await (await _userRepository.GetAllAsync(d =>
                        d.Email == model.Email))
                    .FirstOrDefaultAsync();

            if (person != null)
                return person.ToLoggedDto(GenerateJwtToken(person));

            throw new ArgumentException("Wrong email, or password.");
        }


        private string GenerateJwtToken(
            User user
        )
        {
            return GenerateJwtToken(user.Email, user.UserName,
                user.Id);
        }

        private string GenerateJwtToken(
            string email,
            string login,
            long id
        )
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.UniqueName, login),
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJwt;
        }
    }
}