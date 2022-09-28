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
    public class ReadAllImagenOfProductoAsync : BaseImagenServiceTests
    {
        [Fact]
        public async Task ReadAllImagenOfProducto_10ValidEntries_GetVectorOf10Entries()
        {
            //Arrange
            ResetMockedVariables();
            var productoId = Guid.NewGuid();
            var imagenes = GetEnumerableImagenes(10, productoId).ToList();
            var producto = new Producto
            {
                Id = productoId,
                Imagenes = imagenes
            };
            mockedProductosRepository.Setup(x => x.GetAsync(productoId))
                .ReturnsAsync(producto);

            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);

            //Act
            var resultado = await ImagenesService.ReadAllImagenOfProductoAsync(productoId);

            //Assert
            resultado.Should().HaveCount(10);
            resultado.Should().BeAssignableTo<IEnumerable<DetailsImagenDto>>();

        }

        [Fact]
        public async Task ReadAllImagen_0ValidEntries_GetVectorOf0Entries()
        {
            //Arrange
            ResetMockedVariables();
            var productoId = Guid.NewGuid();
            var imagenes = GetEnumerableImagenes(0, productoId).ToList();
            var producto = new Producto
            {
                Id = productoId,
                Imagenes = imagenes
            };
            mockedProductosRepository.Setup(x => x.GetAsync(productoId))
                .ReturnsAsync(producto);
            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);

            //Act
            var resultado = await ImagenesService.ReadAllImagenOfProductoAsync(productoId);

            //Assert
            resultado.Should().HaveCount(0);
            resultado.Should().BeAssignableTo<IEnumerable<DetailsImagenDto>>();
        }

        [Fact]
        public async Task ReadAllImagen_ProdutoNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var productoId = Guid.NewGuid();
            var imagenes = GetEnumerableImagenes(0, productoId).ToList();
            var producto = new Producto
            {
                Id = productoId,
                Imagenes = imagenes
            };
            SetMockedProductoRepositoryGet(true);
            SetMockedObjects();
            var ImagenesService = new ImagenesService(mockedUnitOfWork.Object);

            //Act
            var resultado = async() => await ImagenesService.ReadAllImagenOfProductoAsync(productoId);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>();
        }

        private IEnumerable<Imagen> GetEnumerableImagenes(int length, Guid ProductoId)
        {
            for(int i = 0; i < length; i++)
            {
                yield return new Imagen
                {
                    Id = Guid.NewGuid(),
                    Nombre = $"Imagen{i+1}",
                    Base64 = $"ContenidoImagen{i+1}",
                    ProductoId = ProductoId,
                    Url = ""
                }; 
            }
        }
    }
}
