using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CityBuilder.Data;
using CityBuilder.Models.CustomeExceptions;
using CityBuilder.Models.DTOs.IdentityDTOs;
using CityBuilder.Models.InputModels.IdentityInputModels;
using CityBuilder.Models.OutputModels.IdentityOutputModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CityBuilder.Services
{
    public class IdentityService
    {

        public IdentityService(CityBuilderDbContext context, IOptions<IdentityConfig> options)
        {
            this.context = context;
            this.options = options;
        }

        private readonly CityBuilderDbContext context;
        private readonly IOptions<IdentityConfig> options;

        public async Task<LoginOutputModel> Login(LoginInputModel loginInputModel)
        {
            if (!(await this.context.Users.AnyAsync(u => u.Username == loginInputModel.Username && u.Password == loginInputModel.Password)))
            {
                throw new UnauthorizedException("Wrong username or password");
            }

            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Username == loginInputModel.Username && u.Password == loginInputModel.Password);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, loginInputModel.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            var loginResponse = new LoginOutputModel()
            {
                Token = encryptedToken
            };

            return loginResponse;
        }
    }
}
