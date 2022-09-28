using CatalogoAranda.ApplicationCore.Dtos.AuthenticationDtos;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace CatalogoAranda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<CatalogoUser> signInManager;
        private readonly UserManager<CatalogoUser> userManager;

        public AuthenticationController(SignInManager<CatalogoUser> signInManager,
            UserManager<CatalogoUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                return StatusCode(StatusCodes.Status500InternalServerError, resultado.Errors);
            }
        }

        [Route("IniciarSesion")]
        [HttpPost]
        public async Task<LoggedDto> IniciarSesionAsync([FromBody] AutenticacionDto usuarioAAutenticar)
        {
            var resultadoInicioSesion = await signInManager.PasswordSignInAsync(
                usuarioAAutenticar.NombreUsuario, usuarioAAutenticar.Password,
                false, false);
        }
    }
}
