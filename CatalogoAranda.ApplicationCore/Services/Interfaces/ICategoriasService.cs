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
        Task<bool> CreateCategoriaAsync(CreateCategoriaDto createCategoriaDto);

        Task<bool> UpdateCategoriaAsync(UpdateCategoriaDto updateCategoriaDto);

        Task<DetailsCategoriaDto> ReadCategoriaAsync(Guid Id);

        Task<bool> DeleteCategoriaAsync(Guid Id);
    }
}
