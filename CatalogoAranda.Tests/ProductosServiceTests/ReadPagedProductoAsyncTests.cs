using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Dtos.ProductosDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.ProductosServiceTests
{
    public class ReadPagedProductoAsyncTests : BaseImagenServiceTests
    {
        [Fact]
        public async Task ReadPagedProducto_ValidSearch_ReturnsPageAllElements()
        {
            //Arrange
            ResetMockedVariables();
            int recordsPerPage = 10;
            int page = 1;
            mockedProductosRepository.Setup(x => x.GetManyAsync(
                It.IsAny<IEnumerable<Expression<Func<Producto, bool>>>>(),
                It.IsAny<Func<IQueryable<Producto>, IOrderedQueryable<Producto>>>(), page, recordsPerPage
                ))
                .ReturnsAsync(GenerateRandomProducts(recordsPerPage));
            SetMockedObjects();
            var productosService = new ProductosService(mockedUnitOfWork.Object, mockedCategoriaService.Object);

            //Act
            var resultado = await productosService.ReadPagedProductoAsync(
                "10","11","Cat1",null,null, page, recordsPerPage);

            //Assert
            resultado.Should().HaveCountLessThanOrEqualTo(recordsPerPage);
            resultado.Should().BeAssignableTo<IEnumerable<DetailsProductoDto>>();
        }

        [Fact]
        public async Task ReadPagedProducto_ValidSearch_ReturnsPageNoElements()
        {
            //Arrange
            ResetMockedVariables();
            int recordsPerPage = 10;
            int page = 1;
            mockedProductosRepository.Setup(x => x.GetManyAsync(
                It.IsAny<IEnumerable<Expression<Func<Producto, bool>>>>(),
                It.IsAny<Func<IQueryable<Producto>, IOrderedQueryable<Producto>>>(), page, recordsPerPage
                ))
                .ReturnsAsync(GenerateRandomProducts(0));
            SetMockedObjects();
            var productosService = new ProductosService(mockedUnitOfWork.Object, mockedCategoriaService.Object);

            //Act
            var resultado = await productosService.ReadPagedProductoAsync(
                "10", "11", "Cat1", null, null,page, recordsPerPage);

            //Assert
            resultado.Should().HaveCountLessThanOrEqualTo(recordsPerPage);
            resultado.Should().BeAssignableTo<IEnumerable<DetailsProductoDto>>();
        }

        [Fact]
        public async Task ReadPagedProducto_InvalidSearch_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            int recordsPerPage = 10;
            int page = 1;
            mockedProductosRepository.Setup(x => x.GetManyAsync(
                It.IsAny<IEnumerable<Expression<Func<Producto, bool>>>>(),
                It.IsAny<Func<IQueryable<Producto>, IOrderedQueryable<Producto>>>(), page, recordsPerPage
                ))
                .ThrowsAsync(new ArgumentException("Búsqueda inválida."));
            SetMockedObjects();
            var productosService = new ProductosService(mockedUnitOfWork.Object, mockedCategoriaService.Object);

            //Act
            var resultado = async () => await productosService.ReadPagedProductoAsync(
                "10", "11", "Cat1", null, null, page, recordsPerPage);

            //Assert
            await resultado.Should().ThrowAsync<ArgumentException>();
        }
        private IEnumerable<Producto> GenerateRandomProducts(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return new Producto
                {
                    Id = Guid.NewGuid(),
                    Nombre = $"ProductoPrueba{i+10}",
                    Descripcion = $"Descripción{i+10} categoría de prueba",
                    Categoria = new List<Categoria>(),
                    Imagenes = new List<Imagen>()
                };
            }
        }


    }
}
