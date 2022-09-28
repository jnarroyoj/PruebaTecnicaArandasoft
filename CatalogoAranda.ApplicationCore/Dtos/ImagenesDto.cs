using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Dtos
{
    public record CreateImagenDto(string Nombre,
        string ImageContentBase64, Guid ProductoId);

    public record UpdateImagenDto(Guid Id, string Nombre,
        string Url, string ImageContentBase64, Guid ProductoId);

    public record DetailsImagenDto(Guid Id, string Nombre,
        string Url, string ImageContentBase64, Guid ProductoId);
}
