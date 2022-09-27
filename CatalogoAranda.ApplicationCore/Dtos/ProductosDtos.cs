using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Dtos.ProductosDtos
{
    public record CreateProductoDto(string Nombre,
        string Descripcion, Guid[] Categorias, Guid[] Imagenes    
        );

    public record UpdateProductoDto(Guid Id, string Nombre,
        string Descripcion, Guid[] Categorias, Guid[] Imagenes);


}
