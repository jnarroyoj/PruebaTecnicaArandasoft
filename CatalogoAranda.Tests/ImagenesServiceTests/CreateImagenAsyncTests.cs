using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Services;
using Moq;
using FluentAssertions;
using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAranda.Tests.ImagenesServiceTests
{
    public class CreateImagenAsyncTests : BaseImagenServiceTests
    {
        
        public CreateImagenAsyncTests()
        {
        }

        private Func<CreateImagenDto, Task<string>> GetBucketUploadImage(CreateImagenDto createImagenDto, bool validUrl)
        {
            if(validUrl) return async (createImagenDto) => "http://bucketejemplo/imagentest.jpg";
            else return async (createImagenDto) => "";
        }

        [Fact]
        public async Task CreateImagen_ImagenValidaSinBucket_ReturnsCreatedObject()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(true);
            SetMockedImagenRepositoryGet(false);
            SetMockedProductoRepositoryGet(false);
            bool validUrlFromBucket = false;
            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);
            var Imagen = new CreateImagenDto("ImagenDePrueba",
                "Contenido de Imagen de prueba", Guid.NewGuid());

            //Act
            var resultado = await ImagenesService.CreateImagenAsync(Imagen,
                GetBucketUploadImage(Imagen, validUrlFromBucket));

            //Assert
            resultado.Should().BeAssignableTo<DetailsImagenDto>();
            resultado.Url.Should().BeNullOrEmpty();
            resultado.ImageContentBase64.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task CreateImagen_ImagenValidaConBucket_ReturnsCreatedObject()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(true);
            SetMockedImagenRepositoryGet(false);
            SetMockedProductoRepositoryGet(false);
            bool validUrlFromBucket = true;
            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);
            var Imagen = new CreateImagenDto("ImagenDePrueba",
                "Contenido de Imagen de prueba", Guid.NewGuid());

            //Act
            var resultado = await ImagenesService.CreateImagenAsync(Imagen,
                GetBucketUploadImage(Imagen, validUrlFromBucket));

            //Assert
            resultado.Should().BeAssignableTo<DetailsImagenDto>();
            resultado.ImageContentBase64.Should().BeNullOrEmpty();
            resultado.Url.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task CreateImagen_ImagenInválida_ThrowsException()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(true);
            SetMockedImagenRepositoryGet(false);
            SetMockedProductoRepositoryGet(false);
            SetSaveChangesExceptionAsync();
            bool validUrlFromBucket = false;
            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);
            var Imagen = new CreateImagenDto("ImagenDePrueba",
                "Contenido de Imagen de prueba", Guid.NewGuid());

            //Act
            var resultado = async () => await ImagenesService.CreateImagenAsync(Imagen,
                GetBucketUploadImage(Imagen, validUrlFromBucket));

            //Assert
            await resultado.Should().ThrowAsync<DbUpdateException>();
        }

        [Fact]
        public async Task CreateImagen_GUIDInválido_ThrowsException()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(false);
            SetMockedImagenRepositoryGet(false);
            SetMockedProductoRepositoryGet(false);
            bool validUrlFromBucket = false;
            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);
            var Imagen = new CreateImagenDto("ImagenDePrueba",
                "Contenido de Imagen de prueba", Guid.NewGuid());

            //Act
            var resultado = async () => await ImagenesService.CreateImagenAsync(Imagen,
                GetBucketUploadImage(Imagen, validUrlFromBucket));

            //Assert
            await resultado.Should().ThrowAsync<Exception>("No se encontró un GUID válido luego de 5 intentos.");
        }
    }
}