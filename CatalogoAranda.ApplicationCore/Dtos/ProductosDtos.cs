using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Dtos.ProductosDtos
{
    public record CreateProductoDto([MaxLength(250)] string Nombre,
        [MaxLength(1000)] string Descripcion, Guid[] Categorias    
        );

    public record UpdateProductoDto(Guid Id, [MaxLength(250)] string Nombre,
        [MaxLength(1000)] string Descripcion, Guid[] Categorias);

    public record DetailsProductoDto(Guid Id, string Nombre,
        string Descripcion, DetailsCategoriaDto[] Categorias, Guid[] Imagenes);

    public record PagedDetailsProductoDto(IEnumerable<DetailsProductoDto> Productos,
        int CantidadTotalDeProductos);

}
