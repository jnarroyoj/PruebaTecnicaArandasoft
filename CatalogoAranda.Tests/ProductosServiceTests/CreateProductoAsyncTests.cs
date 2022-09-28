using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
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
    public class CreateProductoAsyncTests : BaseImagenServiceTests
    {
        [Fact]
        public async Task CreateProducto_ValidProducto_ReturnsDetailsProducto()
        {
            //Arrange 
            ResetMockedVariables();
            SetIdNotExists(true);
            var testProduct = CreateTestProduct();
            var createProductDto = new CreateProductoDto(testProduct.Nombre,
                testProduct.Descripcion, new Guid[0]);
            var detailsProductoDto = new DetailsProductoDto(testProduct.Id,
                testProduct.Nombre, testProduct.Descripcion, new DetailsCategoriaDto[0], new Guid[0]);
            mockedProductosRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(testProduct);
            SetGetAsyncForRepositories();
            SetMockedObjects();
            var productosService = new ProductosService(mockedUnitOfWork.Object,
                mockedCategoriaService.Object);

            //Act
            var resultado = await productosService.CreateProductoAsync(createProductDto);

            //Assert
            resultado.Should().BeEquivalentTo(detailsProductoDto);

        }

        [Fact]
        public async Task CreateProducto_ProductoInválido_ThrowsDbUpdateException()
        {
            //Arrange 
            ResetMockedVariables();
            SetIdNotExists(true);
            SetSaveChangesExceptionAsync();
            var testProduct = CreateTestProduct();
            var createProductDto = new CreateProductoDto(testProduct.Nombre,
                testProduct.Descripcion, new Guid[0]);
            var detailsProductoDto = new DetailsProductoDto(testProduct.Id,
                testProduct.Nombre, testProduct.Descripcion, new DetailsCategoriaDto[0], new Guid[0]);
            mockedProductosRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(testProduct);
            SetGetAsyncForRepositories();
            SetMockedObjects();
            var productosService = new ProductosService(mockedUnitOfWork.Object,
                mockedCategoriaService.Object);

            //Act
            var resultado = async () => await productosService.CreateProductoAsync(createProductDto);

            //Assert
            await resultado.Should().ThrowAsync<DbUpdateException>();

        }

        [Fact]
        public async Task CreateProducto_GuidInválido_ThrowsException()
        {
            //Arrange 
            ResetMockedVariables();
            SetIdNotExists(false);
            var testProduct = CreateTestProduct();
            var createProductDto = new CreateProductoDto(testProduct.Nombre,
                testProduct.Descripcion, new Guid[0]);
            var detailsProductoDto = new DetailsProductoDto(testProduct.Id,
                testProduct.Nombre, testProduct.Descripcion, new DetailsCategoriaDto[0], new Guid[0]);
            mockedProductosRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(testProduct);
            SetGetAsyncForRepositories();
            SetMockedObjects();
            var productosService = new ProductosService(mockedUnitOfWork.Object,
                mockedCategoriaService.Object);

            //Act
            var resultado = async () => await productosService.CreateProductoAsync(createProductDto);

            //Assert
            await resultado.Should().ThrowAsync<Exception>();

        }
        private void SetIdNotExists(bool result)
        {
            mockedProductosRepository.Setup(
                x => x.IdNotExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(result);
        }
    }
}
