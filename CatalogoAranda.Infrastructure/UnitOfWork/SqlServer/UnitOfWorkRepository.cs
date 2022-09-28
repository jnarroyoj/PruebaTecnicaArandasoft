using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.Infrastructure.Data;
using CatalogoAranda.Infrastructure.Repositories.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Infrastructure.UnitOfWork.SqlServer
{
    internal class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IProductosRepository ProductosRepository {get;}

        public ICategoriasRepository CategoriasRepository { get; }

        public IImagenesRepository ImagenesRepository { get;  }

        public UnitOfWorkRepository(CatalogoDbContext contexto)
        {
            this.CategoriasRepository = new CategoriasRepository(contexto);
            this.ProductosRepository = new ProductosRepository(contexto);
            this.ImagenesRepository = new ImagenesRepository(contexto);
        }
    }
}
