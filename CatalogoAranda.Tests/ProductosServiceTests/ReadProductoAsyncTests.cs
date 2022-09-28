using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;
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
    public class ReadProductoAsyncTests : BaseProductoServiceTests
    {
        [Fact]
        public async Task ReadProducto_IdExiste_ReturnsDetailsProductoDto()
        {
            //Arrange
            ResetMockedVariables();
            Producto ProductoTest = CreateTestProduct();
            var Id = ProductoTest.Id;

            var detalleProductoDto = new DetailsProductoDto(Id, ProductoTest.Nombre
                ,ProductoTest.Descripcion, new DetailsCategoriaDto[0],
                new Guid[0]);
            mockedProductosRepository.Setup(x => x.GetAsync(Id)).ReturnsAsync(ProductoTest);
            SetMockedObjects();

            var ProductosService = new ProductosService(mockedUnitOfWork.Object, mockedCategoriaService.Object);

            //Act
            var resultado = await ProductosService.ReadProductoAsync(Id);

            //Assert
            resultado.Should().BeEquivalentTo(detalleProductoDto);
        }

        [Fact]
        
        public async Task ReadProducto_IdNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var Id = Guid.NewGuid();
            SetMockedObjects();

            var ProductosService = new ProductosService(mockedUnitOfWork.Object, mockedCategoriaService.Object);

            //Act
            var resultado = async () => await ProductosService.ReadProductoAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("El producto no existe.");
        }
    }
}
