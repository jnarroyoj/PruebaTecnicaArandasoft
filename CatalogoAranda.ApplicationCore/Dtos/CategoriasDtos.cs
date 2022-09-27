using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos
{
    public record CreateCategoriaDto(string Nombre,
    string Descripcion);

    public record UpdateCategoriaDto(Guid Id, string Nombre,
        string Descripcion);

    public record DetailsCategoriaDto(Guid Id, string Nombre,
        string Descripcion);
}
