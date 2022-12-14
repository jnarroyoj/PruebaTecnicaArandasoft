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

namespace CatalogoAranda.Tests.ProductosServiceTests
{
    public class DeleteProductoAsyncTests : BaseImagenServiceTests
    {
        [Fact]
        public async Task DeleteProducto_GuidExiste_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedProductoRepositoryGet(false);
            SetMockedObjects();
            var ProductosService = InitializeProductosService();
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
            var ProductosService = InitializeProductosService();
            var Id = Guid.NewGuid();

            //Act
            var resultado = async () => await ProductosService.DeleteProductoAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("La categoría no existe.");
        }
        [Fact]
        public async Task DeleteProducto_BorradoInválido_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedProductoRepositoryGet(false);
            SetSaveChangesExceptionAsync();
            SetMockedObjects();
            var ProductosService = InitializeProductosService();
            var Id = Guid.NewGuid();

            //Act
            var resultado = async () => await ProductosService.DeleteProductoAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
