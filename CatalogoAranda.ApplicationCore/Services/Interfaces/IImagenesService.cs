using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;

namespace CatalogoAranda.ApplicationCore.Services.Interfaces
{
    public interface IImagenesService
    {
        Task<DetailsImagenDto> CreateImagenAsync(CreateImagenDto createImagenDto,
            Func<CreateImagenDto, Task<string>> UploadImageToBucket);

        Task<DetailsImagenDto> ReadImagenAsync(Guid Id);

        Task<IEnumerable<DetailsImagenDto>> ReadAllImagenOfProductoAsync(Guid ProductId);

        Task DeleteImagenAsync(Guid Id, Func<DetailsImagenDto, Task> DeleteImageFromBucket);
    }
}
