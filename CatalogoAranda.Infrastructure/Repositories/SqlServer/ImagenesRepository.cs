using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Infrastructure.Repositories.SqlServer
{
    public class ImagenesRepository : IImagenesRepository
    {
        private readonly CatalogoDbContext contexto;

        public ImagenesRepository(CatalogoDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task CreateAsync(Imagen t)
        {
            await contexto.Imagenes.AddAsync(t);
        }

        public async Task DeleteAsync(Imagen Id)
        {
            await Task.Run(() => contexto.Imagenes.Remove(Id));
        }

        public async Task<IEnumerable<Imagen>> GetAllAsync()
        {
            return await Task.Run(() => contexto.Imagenes);
        }

        public async Task<Imagen?> GetAsync(Guid Id)
        {
            return await contexto.Imagenes.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> IdNotExistsAsync(Guid Id)
        {
            return await contexto.Imagenes.Where(x => x.Id == Id).CountAsync() == 0;
        }
    }
}
