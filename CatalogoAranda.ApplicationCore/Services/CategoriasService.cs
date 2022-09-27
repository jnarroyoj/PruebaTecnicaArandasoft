using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Dtos.CategoriasDtos;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.Services
{
    public class CategoriasService : BaseService, ICategoriasService
    {
        private readonly IUnitOfWorkAdapter unitOfWorkAdapter;
        private readonly ICategoriasRepository categoriasRepository;

        public CategoriasService(IUnitOfWork unitOfWork)
        {
            this.unitOfWorkAdapter = unitOfWork.Create();
            categoriasRepository = unitOfWorkAdapter.Repositories.CategoriasRepository;
        }
        public async Task<bool> CreateCategoriaAsync(CreateCategoriaDto createCategoriaDto)
        {
            Guid Id = await GetValidGuid(categoriasRepository.IdNotExists);

            var categoria = new Categoria
            {
                Id = Id,
                Nombre = createCategoriaDto.Nombre,
                Descripcion = createCategoriaDto.Descripcion
            };

            await categoriasRepository.CreateAsync(categoria);

            await unitOfWorkAdapter.SaveChangesAsync();

            return true;
        }

        public Task<bool> DeleteCategoriaAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<DetailsCategoriaDto> ReadCategoriaAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoriaAsync(UpdateCategoriaDto updateCategoriaDto)
        {
            throw new NotImplementedException();
        }
    }
}
