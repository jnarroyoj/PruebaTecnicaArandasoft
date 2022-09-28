using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.CategoriasServiceTests
{
    public class ReadCategoriaAsyncTests : BaseCategoriaServiceTests
    {
        [Fact]
        public async Task ReadCategoria_IdExiste_ReturnsDetailsCategoriaDto()
        {
            //Arrange
            ResetMockedVariables();
            var Id = Guid.NewGuid();
            Categoria? categoriaTest = new Categoria{Id = Id,
                Nombre = "CategoriaPrueba",
                Descripcion = "Descripción categoría de prueba" };
            var detalleCategoriaDto = new DetailsCategoriaDto(Id, categoriaTest.Nombre, categoriaTest.Descripcion);
            mockedCategoriasRepository.Setup(x => x.GetAsync(Id)).ReturnsAsync(categoriaTest);
            SetMockedObjects();

            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = await categoriasService.ReadCategoriaAsync(Id);

            //Assert
            resultado.Should().BeEquivalentTo(detalleCategoriaDto);
        }

        [Fact]
        public async Task ReadCategoria_IdNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var Id = Guid.NewGuid();
            SetMockedObjects();

            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = async () => await categoriasService.ReadCategoriaAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("La categoría no existe.");
        }
    }
}
