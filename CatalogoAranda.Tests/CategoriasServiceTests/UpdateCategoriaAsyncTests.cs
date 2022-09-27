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
    public class UpdateCategoriaAsyncTests : BaseCategoriaServiceTests
    {
        UpdateCategoriaDto RandomUpdateCategoriaDto()
        {
            return new UpdateCategoriaDto(Guid.NewGuid(), "CategoriaPrueba",
                "Prueba de actualización de categoría");
        }
        [Fact]
        public async Task UpdateCategoria_CategoriaVálida_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var categoria = RandomUpdateCategoriaDto();
            SetMockedCategoriaRepositoryGet(false);
            SetMockedObjects();

            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = async () => await categoriasService.UpdateCategoriaAsync(categoria);

            //Assert
            await resultado.Should().NotThrowAsync();
        }
        [Fact]
        public async Task UpdateCategoria_CategoriaNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var categoria = RandomUpdateCategoriaDto();
            SetMockedCategoriaRepositoryGet(true);
            SetMockedObjects();

            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = async () => await categoriasService.UpdateCategoriaAsync(categoria);

            //Assert
            await resultado.Should().ThrowAsync<Exception>("La categoría no existe.");
        }
        [Fact]
        public async Task UpdateCategoria_NombreCategoriaYaExiste_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            var categoria = RandomUpdateCategoriaDto();
            SetMockedCategoriaRepositoryGet(false);
            SetSaveChangesExceptionAsync();
            SetMockedObjects();

            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = async () => await categoriasService.UpdateCategoriaAsync(categoria);

            //Assert
            await resultado.Should().ThrowAsync<Exception>();
        }
    }
}
