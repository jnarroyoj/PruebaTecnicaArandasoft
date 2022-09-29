using CatalogoAranda.ApplicationCore.Dtos.AuthenticationDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.UtilityServices.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace CatalogoAranda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<CatalogoUser> signInManager;
        private readonly UserManager<CatalogoUser> userManager;
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(SignInManager<CatalogoUser> signInManager,
            UserManager<CatalogoUser> userManager, IAuthenticationService authenticationService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.authenticationService = authenticationService;
        }

        [Route("Registrar")]
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuarioAsync([FromBody] AutenticacionDto usuarioARegistrar)
        {
            var usuario = new CatalogoUser();
            usuario.UserName = usuarioARegistrar.NombreUsuario;

            var resultadoRegistro = await userManager.CreateAsync(usuario,
                usuarioARegistrar.Password);

            if (!resultadoRegistro.Succeeded)
            {
                return Conflict(resultadoRegistro.Errors);
            }

            return CreatedAtAction(nameof(IniciarSesionAsync), new { usuarioAAutenticar = usuarioARegistrar },
                await authenticationService.ConstruirJWTAsync(usuarioARegistrar));
        }


        [Route("IniciarSesion")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IniciarSesionAsync([FromBody] AutenticacionDto usuarioAAutenticar)
        {
            var resultadoInicioSesion = await signInManager.PasswordSignInAsync(
                usuarioAAutenticar.NombreUsuario, usuarioAAutenticar.Password,
                false, false);

            if (resultadoInicioSesion.Succeeded)
                return Ok(await authenticationService.ConstruirJWTAsync(usuarioAAutenticar));

            return Unauthorized();
        }
    }
}
