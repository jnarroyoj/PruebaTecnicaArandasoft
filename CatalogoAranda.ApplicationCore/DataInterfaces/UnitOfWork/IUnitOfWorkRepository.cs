using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        IProductosRepository ProductosRepository { get; }
        ICategoriasRepository CategoriasRepository { get; }
        IImagenesRepository ImagenesRepository { get; }


    }
}
