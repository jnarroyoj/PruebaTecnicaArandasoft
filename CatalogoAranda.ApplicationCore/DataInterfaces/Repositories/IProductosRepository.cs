using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions;
using CatalogoAranda.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.DataInterfaces.Repositories
{
    public interface IProductosRepository : ICreateRepository<Producto>,
        IReadPagedRepository<Producto, Guid>, IUpdateRepository<Producto>,
        IDeleteRepository<Producto>
    {

    }
}
