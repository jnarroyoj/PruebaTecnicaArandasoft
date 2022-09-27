using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Infrastructure.UnitOfWork.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogoDbContext contexto;

        public UnitOfWork(CatalogoDbContext contexto)
        {
            this.contexto = contexto;
        }
        public IUnitOfWorkAdapter Create()
        {
            return new UnitOfWorkAdapter(contexto);
        }
    }
}
