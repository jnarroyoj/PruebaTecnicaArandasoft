using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Dtos.AuthenticationDtos
{
    public record AutenticacionDto([Required] string NombreUsuario,
    [MinLength(6)] string Password);

    public record LoggedDto(string NombreUsuario, string JWToken);
}
