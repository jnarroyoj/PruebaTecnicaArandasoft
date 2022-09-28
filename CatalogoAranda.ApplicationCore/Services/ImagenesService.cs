using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CatalogoAranda.ApplicationCore.Services
{
    public class ImagenesService : BaseService, IImagenesService
    {
        private readonly IUnitOfWorkAdapter unitOfWorkAdapter;
        private readonly IImagenesRepository imagenesRepository;
        private readonly IProductosRepository productosRepository;

        public ImagenesService(IUnitOfWork unitOfWork)
        {
            unitOfWorkAdapter = unitOfWork.Create();
            imagenesRepository = unitOfWorkAdapter.Repositories.ImagenesRepository;
            productosRepository = unitOfWorkAdapter.Repositories.ProductosRepository;
        }
        public async Task<DetailsImagenDto> CreateImagenAsync(CreateImagenDto createImagenDto, Func<CreateImagenDto, Task<string>> UploadImageToBucket)
        {
            Guid Id = await GetValidGuidAsync(imagenesRepository.IdNotExistsAsync);

            var producto = await RetrieveProductoAsync(createImagenDto.ProductoId);

            var url = await UploadImageToBucket(createImagenDto);

            bool imageHasValidUrl = Uri.TryCreate(url, UriKind.Absolute, out _);

            var imagen = new Imagen
            {
                Id = Id,
                Nombre = createImagenDto.Nombre,
                Base64 = imageHasValidUrl ? "" : createImagenDto.ImageContentBase64,
                Url = imageHasValidUrl ? url : "",
                ProductoId = createImagenDto.ProductoId,
                Producto = producto
            };

            await imagenesRepository.CreateAsync(imagen);

            await unitOfWorkAdapter.SaveChangesAsync();

            return new DetailsImagenDto(Id, imagen.Nombre,
                imagen.Url, imagen.Base64, imagen.ProductoId);
        }

        public async Task DeleteImagenAsync(Guid Id, Func<DetailsImagenDto, Task> DeleteImageFromBucket)
        {
            var imagen = await RetrieveImagenAsync(Id);

            var detailsImagen = await ReadImagenAsync(Id);

            await imagenesRepository.DeleteAsync(imagen);

            await unitOfWorkAdapter.SaveChangesAsync();

            await DeleteImageFromBucket(detailsImagen);
        }

        public async Task<IEnumerable<DetailsImagenDto>> ReadAllImagenOfProductoAsync(Guid ProductId)
        {
            var producto = await RetrieveProductoAsync(ProductId);

            var detailsImagenesDto = new List<DetailsImagenDto>();

            foreach(var imagen in producto.Imagenes)
            {
                detailsImagenesDto.Add(new DetailsImagenDto(imagen.Id, imagen.Nombre,
                imagen.Url, imagen.Base64, imagen.ProductoId));
            }

            return detailsImagenesDto;
        }

        public async Task<DetailsImagenDto> ReadImagenAsync(Guid Id)
        {
            var imagen = await RetrieveImagenAsync(Id);

            return new DetailsImagenDto(Id, imagen.Nombre,
                imagen.Url, imagen.Base64, imagen.ProductoId);
        }

        private async Task<Imagen> RetrieveImagenAsync(Guid Id)
        {
            var imagen = await imagenesRepository.GetAsync(Id);

            if (imagen is null)
                throw new NullReferenceException("La imagen no existe.");

            return imagen;
        }

        private async Task<Producto> RetrieveProductoAsync(Guid Id)
        {
            var producto = await productosRepository.GetAsync(Id);

            if (producto is null)
                throw new NullReferenceException("El producto no existe.");

            return producto;
        }
    }
}
