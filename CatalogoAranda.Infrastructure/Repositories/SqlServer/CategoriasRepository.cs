using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Infrastructure.Repositories.SqlServer
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly CatalogoDbContext contexto;
        public CategoriasRepository(CatalogoDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task CreateAsync(Categoria t)
        {
            await contexto.Categorias.AddAsync(t);
        }

        public async Task DeleteAsync(Categoria Id)
        {
            await Task.Run(() => contexto.Categorias.Remove(Id));
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await Task.Run(() => contexto.Categorias);
        }

        public async Task<IEnumerable<Producto>> GetAllProductosWithCategoriaAsync(Guid IdCategoria)
        {
            var categoriaConProductos = await contexto.Categorias
                .Where(categoria => categoria.Id == IdCategoria)
                .Include(categoria => categoria.Productos)
                .SingleAsync();

            return categoriaConProductos.Productos;
        }

        public async Task<Categoria?> GetAsync(Guid Id)
        {
            return await contexto.Categorias.Where(x => Id == x.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> IdNotExistsAsync(Guid Id)
        {
            return (await contexto.Categorias.CountAsync(x => x.Id == Id)) == 0;
        }

        public async Task UpdateAsync(Categoria t)
        {
            await Task.Run(() => contexto.Categorias.Update(t));
        }
    }
}
