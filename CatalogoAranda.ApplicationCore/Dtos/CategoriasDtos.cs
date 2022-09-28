using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos
{
    public record CreateCategoriaDto([MaxLength(250)] string Nombre,
        string Descripcion);

    public record UpdateCategoriaDto(Guid Id, [MaxLength(250)] string Nombre,
        string Descripcion);

    public record DetailsCategoriaDto(Guid Id, string Nombre,
        string Descripcion);
}
