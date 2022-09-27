using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Services;
using Moq;
using FluentAssertions;
using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;

namespace CatalogoAranda.Tests.CategoriasServiceTests
{
    public class CreateCategoriaAsyncTests
    {
        Mock<IUnitOfWork> mockedUnitOfWork = new();
        Mock<IUnitOfWorkAdapter> mockedUnitOfWorkAdapter = new();
        Mock<IUnitOfWorkRepository> mockedUnitOfWorkRepository = new();
        Mock<ICategoriasRepository> mockedCategoriasRepository = new();
        public CreateCategoriaAsyncTests()
        {
        }

        private void ResetMockedVariables()
        {
            mockedUnitOfWork = new();
            mockedUnitOfWorkAdapter = new();
            mockedUnitOfWorkRepository = new();
            mockedCategoriasRepository = new();
        }

        private void SetSaveChangesExceptionAsync()
        {
            mockedUnitOfWorkAdapter.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new Exception("Guardado Inválido"));
        }

        private void SetIdNotExists(bool result)
        {
            mockedCategoriasRepository.Setup(
                x => x.IdNotExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(result);
        }

        private void SetMockedObjects()
        {
            mockedUnitOfWorkRepository.SetupGet(x => x.CategoriasRepository).Returns(mockedCategoriasRepository.Object);
            mockedUnitOfWorkAdapter.SetupGet(x => x.Repositories).Returns(mockedUnitOfWorkRepository.Object);
            mockedUnitOfWork.Setup(x => x.Create()).Returns(mockedUnitOfWorkAdapter.Object);
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
            var resultado = await categoriasService.CreateCategoriaAsync(categoria);

            //Assert
            resultado.Should().Be(true);
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