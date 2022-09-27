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
    public class ReadAllCategoriaAsync : BaseCategoriaServiceTests
    {
        [Fact]
        public async Task ReadAllCategoria_10ValidEntries_GetVectorOf10Entries()
        {
            //Arrange
            ResetMockedVariables();
            mockedCategoriasRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(GetEnumerableCategorias(10));
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = await categoriasService.ReadAllCategoriaAsync();

            //Assert
            resultado.Should().HaveCount(10);
            resultado.Should().BeAssignableTo<IEnumerable<DetailsCategoriaDto>>();

        }

        [Fact]
        public async Task ReadAllCategoria_0ValidEntries_GetVectorOf0Entries()
        {
            //Arrange
            ResetMockedVariables();
            mockedCategoriasRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(GetEnumerableCategorias(0));
            SetMockedObjects();
            var categoriasService = new CategoriasService(mockedUnitOfWork.Object);

            //Act
            var resultado = await categoriasService.ReadAllCategoriaAsync();

            //Assert
            resultado.Should().HaveCount(0);
            resultado.Should().BeAssignableTo<IEnumerable<DetailsCategoriaDto>>();
        }

        private IEnumerable<Categoria> GetEnumerableCategorias(int length)
        {
            for(int i = 0; i < length; i++)
            {
                yield return new Categoria
                {
                    Id = Guid.NewGuid(),
                    Nombre = $"Categoria{i+1}",
                    Descripcion = $"Descripción de categoría número {i+1}"
                }; 
            }
        }
    }
}
