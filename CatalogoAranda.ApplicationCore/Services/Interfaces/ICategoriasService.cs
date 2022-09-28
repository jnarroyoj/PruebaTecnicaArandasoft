using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;

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
