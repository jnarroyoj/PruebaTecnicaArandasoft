using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;
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
    public class UpdateProductoAsyncTests : BaseImagenServiceTests
    {
        UpdateProductoDto RandomUpdateProductoDto()
        {
            return new UpdateProductoDto(Guid.NewGuid(), "ProductoPrueba",
                "Prueba de actualización de producto", new Guid[0]);
        }

        [Fact]
        public async Task UpdateProducto_ProductoVálida_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var Producto = RandomUpdateProductoDto();
            SetMockedProductoRepositoryGet(false);
            SetGetAsyncForRepositories();
            SetMockedObjects();

            var ProductosService = InitializeProductosService();

            //Act
            var resultado = async () => await ProductosService.UpdateProductoAsync(Producto);

            //Assert
            await resultado.Should().NotThrowAsync();
        }
        [Fact]
        public async Task UpdateProducto_ProductoNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var Producto = RandomUpdateProductoDto();
            SetMockedProductoRepositoryGet(true);
            SetGetAsyncForRepositories();
            SetMockedObjects();

            var ProductosService = InitializeProductosService();

            //Act
            var resultado = async () => await ProductosService.UpdateProductoAsync(Producto);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("La categoría no existe.");
        }
        [Fact]
        public async Task UpdateProducto_NombreProductoYaExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var Producto = RandomUpdateProductoDto();
            SetMockedProductoRepositoryGet(false);
            SetSaveChangesExceptionAsync();
            SetGetAsyncForRepositories();
            SetMockedObjects();

            var ProductosService = InitializeProductosService();

            //Act
            var resultado = async () => await ProductosService.UpdateProductoAsync(Producto);

            //Assert
            await resultado.Should().ThrowAsync<DbUpdateException>();
        }
    }
}
