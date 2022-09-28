using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Infrastructure.Repositories.SqlServer
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly CatalogoDbContext contexto;

        public ProductosRepository(CatalogoDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task CreateAsync(Producto t)
        {
            await contexto.Productos.AddAsync(t);
        }

        public async Task DeleteAsync(Producto Id)
        {
            await Task.Run(() => contexto.Productos.Remove(Id));
        }

        public async Task<Producto?> GetAsync(Guid Id)
        {
            return await contexto.Productos.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Producto>> GetManyAsync(IEnumerable<Expression<Func<Producto, bool>>> filters, Func<IQueryable<Producto>, IOrderedQueryable<Producto>>? orderBy, int page, int recordsPerPage)
        {
            if (page <= 0 || recordsPerPage <= 0) return Array.Empty<Producto>();

            int recordsToSkip = (page - 1) * recordsPerPage;
            var query = await Task.Run(() =>
            {
                var query = contexto.Productos.AsQueryable();
                foreach(var filter in filters)
                {
                    query = query.Where(filter);
                }
                query.Skip(recordsToSkip);
                query.Take(recordsPerPage);

                return query;
            });

            if(orderBy is not null)
            {
                return await orderBy(query).ToArrayAsync();
            }
            return await query.ToArrayAsync();
            
        }

        public async Task<int> GetTotalOfRecordsAsync()
        {
            return await contexto.Productos.CountAsync();
        }

        public async Task<bool> IdNotExistsAsync(Guid Id)
        {
            return (await contexto.Productos.CountAsync(x => x.Id == Id)) == 0;
        }

        public async Task UpdateAsync(Producto t)
        {
            await Task.Run(() => contexto.Productos.Update(t));
        }
    }
}
