using CatalogoAranda.ApplicationCore.Dtos.AuthenticationDtos;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.UtilityServices.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoggedDto> ConstruirJWTAsync(AutenticacionDto autenticacionDto);
    }
}
