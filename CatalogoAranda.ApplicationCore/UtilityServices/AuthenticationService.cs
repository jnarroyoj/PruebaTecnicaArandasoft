using CatalogoAranda.ApplicationCore.Dtos.AuthenticationDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.UtilityServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.UtilityServices
{
    public class AuthenticationService :IAuthenticationService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<CatalogoUser> userManager;

        public AuthenticationService(IConfiguration configuration, UserManager<CatalogoUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<LoggedDto> ConstruirJWTAsync(AutenticacionDto userDto)
        {            
            var claims = new List<Claim>
            {
                new Claim("UserName", userDto.NombreUsuario),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            var user = await userManager.FindByNameAsync(userDto.NombreUsuario);
            var userClaims = await userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LlaveSimetrica"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var validTo = DateTime.UtcNow.AddDays(1);
            var securityToken = new JwtSecurityToken(claims: claims, expires: validTo, signingCredentials: credentials);

            return new LoggedDto(
                userDto.NombreUsuario,
                new JwtSecurityTokenHandler().WriteToken(securityToken)
            );
        }
    }
}
