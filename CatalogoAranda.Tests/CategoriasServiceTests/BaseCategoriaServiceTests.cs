using CatalogoAranda.ApplicationCore.ApplicationExceptions;
using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.CategoriasServiceTests
{
    public class BaseCategoriaServiceTests
    {
        protected Mock<IUnitOfWork> mockedUnitOfWork = new();
        protected Mock<IUnitOfWorkAdapter> mockedUnitOfWorkAdapter = new();
        protected Mock<IUnitOfWorkRepository> mockedUnitOfWorkRepository = new();
        protected Mock<ICategoriasRepository> mockedCategoriasRepository = new();

        protected void ResetMockedVariables()
        {
            mockedUnitOfWork = new();
            mockedUnitOfWorkAdapter = new();
            mockedUnitOfWorkRepository = new();
            mockedCategoriasRepository = new();
        }

        protected void SetSaveChangesExceptionAsync()
        {
            mockedUnitOfWorkAdapter.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new CatalogoDbUpdateException("Guardado Inválido"));
        }

        protected void SetMockedObjects()
        {
            mockedUnitOfWorkRepository.SetupGet(x => x.CategoriasRepository).Returns(mockedCategoriasRepository.Object);
            mockedUnitOfWorkAdapter.SetupGet(x => x.Repositories).Returns(mockedUnitOfWorkRepository.Object);
            mockedUnitOfWork.Setup(x => x.Create()).Returns(mockedUnitOfWorkAdapter.Object);
        }

        protected void SetMockedCategoriaRepositoryGet(bool Nulo)
        {
            if (Nulo)
            {
                Categoria? categoriaNula = null;
                mockedCategoriasRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(categoriaNula);
            }
            else
                mockedCategoriasRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Categoria());
        }
    }
}
