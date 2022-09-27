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
    public class DeleteCategoriaAsyncTests : BaseCategoriaServiceTests
    {
        [Fact]
        public async Task DeleteCategoria_GuidExiste_NotThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedCategoriaRepositoryGet(false);
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);
            var Id = Guid.NewGuid();

            //Act
            var resultado = async () => await categoriasService.DeleteCategoriaAsync(Id);

            //Assert
            await resultado.Should().NotThrowAsync();

        }
        [Fact]
        public async Task DeleteCategoria_GuidNoExiste_ThrowsException()
        {
            //Arrange
            ResetMockedVariables();
            SetMockedCategoriaRepositoryGet(true);
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);
            var Id = Guid.NewGuid();

            //Act
            var resultado = async () => await categoriasService.DeleteCategoriaAsync(Id);

            //Assert
            await resultado.Should().ThrowAsync<NullReferenceException>("La categoría no existe.");
        }
    }
}
