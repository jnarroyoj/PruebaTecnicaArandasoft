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
        public async Task CreateCategoriaAsync(CreateCategoriaDto createCategoriaDto)
        {
            Guid Id = await GetValidGuidAsync(categoriasRepository.IdNotExistsAsync);

            var categoria = new Categoria
            {
                Id = Id,
                Nombre = createCategoriaDto.Nombre,
                Descripcion = createCategoriaDto.Descripcion
            };

            await categoriasRepository.CreateAsync(categoria);

            await unitOfWorkAdapter.SaveChangesAsync();
        }

        public async Task DeleteCategoriaAsync(Guid Id)
        {
            var categoria = await RetrieveCategoria(Id);

            await categoriasRepository.DeleteAsync(categoria);

            await unitOfWorkAdapter.SaveChangesAsync();
        }

        public async Task<IEnumerable<DetailsCategoriaDto>> ReadAllCategoriaAsync()
        {
            var categorias = await categoriasRepository.GetAllAsync();
            var detailsCategoriasDto = categorias.Select(x => 
                new DetailsCategoriaDto(x.Id, x.Nombre, x.Descripcion)
            );
            return detailsCategoriasDto;
        }

        public async Task<DetailsCategoriaDto> ReadCategoriaAsync(Guid Id)
        {
            var categoria = await RetrieveCategoria(Id);

            return new DetailsCategoriaDto(categoria.Id,
                categoria.Nombre, categoria.Descripcion);
        }

        public async Task UpdateCategoriaAsync(UpdateCategoriaDto updateCategoriaDto)
        {
            var categoria = await RetrieveCategoria(updateCategoriaDto.Id);

            categoria.Nombre = updateCategoriaDto.Nombre;
            categoria.Descripcion = updateCategoriaDto.Descripcion;

            await categoriasRepository.UpdateAsync(categoria);

            await unitOfWorkAdapter.SaveChangesAsync();
        }

        private async Task<Categoria> RetrieveCategoria(Guid Id)
        {
            var categoria = await categoriasRepository.GetAsync(Id);

            if (categoria is null)
                throw new NullReferenceException("La categoría no existe.");

            return categoria;
        }
    }
}
