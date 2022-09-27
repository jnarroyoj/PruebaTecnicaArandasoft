using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Infrastructure.UnitOfWork.SqlServer
{
    internal class UnitOfWorkAdapter : IUnitOfWorkAdapter
    {
        private readonly CatalogoDbContext contexto;

        public UnitOfWorkAdapter(CatalogoDbContext contexto)
        {
            this.contexto = contexto;
            Repositories = new UnitOfWorkRepository(contexto);
        }
        public IUnitOfWorkRepository Repositories { get; }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await contexto.SaveChangesAsync();
        }
    }
}
