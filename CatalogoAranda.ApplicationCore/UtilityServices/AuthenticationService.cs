using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.UtilityServices
{
    public class AuthenticationService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<CatalogoUser> userManager;

        public AuthenticationService(IConfiguration configuration, UserManager<CatalogoUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }
    }
}
