using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Services;
using Moq;
using FluentAssertions;
using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;

namespace CatalogoAranda.Tests.CategoriasServiceTests
{
    public class CreateCategoriaAsyncTests : BaseCategoriaServiceTests
    {
        
        public CreateCategoriaAsyncTests()
        {
        }
        private void SetIdNotExists(bool result)
        {
            mockedCategoriasRepository.Setup(
                x => x.IdNotExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(result);
        }
        [Fact]
        public async Task CreateCategoria_CategoriaValida_ReturnsTrue()
        {
            // Arrange
            ResetMockedVariables();
            SetIdNotExists(true);
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);
            var categoria = new CreateCategoriaDto(Nombre: "CategoriaDePrueba",
                Descripcion: "Esta es una categoria de prueba");

            //Act
            var resultado = async () => await categoriasService.CreateCategoriaAsync(categoria);

            //Assert
            await resultado.Should().NotThrowAsync();
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
            await resultado.Should().ThrowAsync<Exception>();
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