using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions;
using CatalogoAranda.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.DataInterfaces.Repositories
{
    public interface ICategoriasRepository : ICreateRepository<Categoria>,
        IReadRepository<Categoria, Guid>, IUpdateRepository<Categoria>,
        IDeleteRepository<Categoria>
    {
        Task<IEnumerable<Producto>> GetAllProductosWithCategoriaAsync(Guid IdCategoria);
    }
}
