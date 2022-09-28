using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Services;
using Moq;
using FluentAssertions;
using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAranda.Tests.CategoriasServiceTests
{
    public class CreateImagenAsyncTests : BaseCategoriaServiceTests
    {
        
        public CreateImagenAsyncTests()
        {
        }
        private void SetIdNotExists(bool result)
        {
            mockedCategoriasRepository.Setup(
                x => x.IdNotExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(result);
        }
        [Fact]
        public async Task CreateCategoria_CategoriaValida_ReturnsCreatedObject()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(true);
            mockedCategoriasRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Categoria{
                    Id = Guid.NewGuid(), Nombre = "", Descripcion = "" });

            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);
            var categoria = new CreateCategoriaDto(Nombre: "CategoriaDePrueba",
                Descripcion: "Esta es una categoria de prueba");

            //Act
            var resultado = await categoriasService.CreateCategoriaAsync(categoria);

            //Assert
            resultado.Should().BeAssignableTo<DetailsCategoriaDto>();
        }

        [Fact]
        public async Task CreateCategoria_CategoriaInválida_ThrowsException()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(true);
            SetSaveChangesExceptionAsync();
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);
            var categoria = new CreateCategoriaDto(Nombre: "CategoriaDePrueba",
                Descripcion: "Esta es una categoria de prueba");

            //Act
            var resultado = async () => await categoriasService.CreateCategoriaAsync(categoria);

            //Assert
            await resultado.Should().ThrowAsync<DbUpdateException>();
        }

        [Fact]
        public async Task CreateCategoria_GUIDInválido_ThrowsException()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(false);
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);
            var categoria = new CreateCategoriaDto(Nombre: "CategoriaDePrueba",
                Descripcion: "Esta es una categoria de prueba");

            //Act
            var resultado = async () => await categoriasService.CreateCategoriaAsync(categoria);

            //Assert
            await resultado.Should().ThrowAsync<Exception>("No se encontró un GUID válido luego de 5 intentos.");
        }
    }
}