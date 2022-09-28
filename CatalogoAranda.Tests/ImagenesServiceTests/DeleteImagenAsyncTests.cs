using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.ImagenesServiceTests
{
    public class DeleteProductAsyncTests : BaseImagenServiceTests
    {
        [Fact]
        public async Task DeleteImagen_GuidExiste_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedImagenRepositoryGet(false);
            SetMockedObjects();
            var imagen = CreateTestImagen();
            DetailsImagenDto detailsImagenDto = new(imagen.Id, imagen.Nombre,
                imagen.Url, imagen.Base64, imagen.ProductoId);
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object
                ,null, GetBucketDeleteImage(detailsImagenDto));

            //Act
            var resultado = async () => await ImagenesService.DeleteImagenAsync(imagen.Id);

            //Assert
            await resultado.Should().NotThrowAsync();

        }
        [Fact]
        public async Task DeleteImagen_GuidNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedImagenRepositoryGet(true);
            SetMockedObjects();
            var imagen = CreateTestImagen();
            DetailsImagenDto detailsImagenDto = new(imagen.Id, imagen.Nombre,
                imagen.Url, imagen.Base64, imagen.ProductoId);
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object,
                null, GetBucketDeleteImage(detailsImagenDto));

            //Act
            var resultado = async () => await ImagenesService.DeleteImagenAsync(imagen.Id);

            //Assert
            await resultado.Should().ThrowAsync<Exception>("La categoría no existe.");
        }

        [Fact]
        public async Task DeleteImagen_BorradoInválido_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedImagenRepositoryGet(false);
            SetSaveChangesExceptionAsync();
            SetMockedObjects();
            var imagen = CreateTestImagen();
            DetailsImagenDto detailsImagenDto = new(imagen.Id, imagen.Nombre,
                imagen.Url, imagen.Base64, imagen.ProductoId);
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object,
                null, GetBucketDeleteImage(detailsImagenDto));

            //Act
            var resultado = async () => await ImagenesService.DeleteImagenAsync(imagen.Id);

            //Assert
            await resultado.Should().ThrowAsync<DbUpdateException>();
        }

        private Func<DetailsImagenDto, Task> GetBucketDeleteImage(DetailsImagenDto detailsImagenDto)
        {
            return async (detailsImagenDto) => { await Task.CompletedTask; };
        }
    }
}
