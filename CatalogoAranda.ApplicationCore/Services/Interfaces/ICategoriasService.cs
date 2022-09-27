using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Services.Interfaces
{
    public interface ICategoriasService
    {
        Task<DetailsCategoriaDto> CreateCategoriaAsync(CreateCategoriaDto createCategoriaDto);

        Task UpdateCategoriaAsync(UpdateCategoriaDto updateCategoriaDto);

        Task<DetailsCategoriaDto> ReadCategoriaAsync(Guid Id);

        Task<IEnumerable<DetailsCategoriaDto>> ReadAllCategoriaAsync();

        Task DeleteCategoriaAsync(Guid Id);
    }
}
