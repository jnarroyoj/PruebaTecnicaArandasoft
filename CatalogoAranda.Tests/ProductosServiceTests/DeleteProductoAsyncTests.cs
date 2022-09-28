using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.ProductosServiceTests
{
    public class DeleteProductoAsyncTests : BaseProductoServiceTests
    {
        [Fact]
        public async Task DeleteProducto_GuidExiste_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedProductoRepositoryGet(false);
            SetMockedObjects();
            var ProductosService = new ProductosService(mockedUnitOfWork.Object,
                mockedCategoriaService.Object);
            var Id = Guid.NewGuid();

            //Act
            var resultado = async () => await ProductosService.DeleteProductoAsync(Id);

            //Assert
            await resultado.Should().NotThrowAsync();

        }
        [Fact]
        public async Task DeleteProducto_GuidNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedProductoRepositoryGet(true);
            SetMockedObjects();
            var ProductosService = new ProductosService(mockedUnitOfWork.Object,
                mockedCategoriaService.Object);
            var Id = Guid.NewGuid();

            //Act
            var resultado = async () => await ProductosService.DeleteProductoAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("La categoría no existe.");
        }
    }
}
