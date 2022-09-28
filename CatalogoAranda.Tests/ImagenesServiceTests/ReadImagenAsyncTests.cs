using CatalogoAranda.ApplicationCore.Dtos.ImagenesDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.ImagenesServiceTests
{
    public class ReadImagenAsyncTests : BaseImagenServiceTests
    {
        [Fact]
        public async Task ReadImagen_IdExiste_ReturnsDetailsImagenDto()
        {
            //Arrange
            ResetMockedVariables();
            var Id = Guid.NewGuid();
            Imagen ImagenTest = CreateTestImagen();
            var detalleImagenDto = new DetailsImagenDto(Id, 
                ImagenTest.Nombre, ImagenTest.Url, 
                ImagenTest.Base64, ImagenTest.ProductoId);
            mockedImagenesRepository.Setup(x => x.GetAsync(Id)).ReturnsAsync(ImagenTest);
            SetMockedObjects();

            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);

            //Act
            var resultado = await ImagenesService.ReadImagenAsync(Id);

            //Assert
            resultado.Should().BeEquivalentTo(detalleImagenDto);
        }

        [Fact]
        public async Task ReadImagen_IdNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var Id = Guid.NewGuid();
            SetMockedImagenRepositoryGet(true);
            SetMockedObjects();

            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);

            //Act
            var resultado = async () => await ImagenesService.ReadImagenAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("La categoría no existe.");
        }
    }
}
