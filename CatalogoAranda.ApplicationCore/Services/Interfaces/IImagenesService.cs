using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;

namespace CatalogoAranda.ApplicationCore.Services.Interfaces
{
    public interface IImagenesService
    {
        Task<DetailsImagenDto> CreateImagenAsync(CreateImagenDto createImagenDto);

        Task<DetailsImagenDto> ReadImagenAsync(Guid Id);

        Task<IEnumerable<DetailsImagenDto>> ReadAllImagenOfProductoAsync(Guid ProductId);

        Task DeleteImagenAsync(Guid Id);
    }
}
