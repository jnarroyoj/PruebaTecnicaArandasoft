using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions
{
    public interface IDeleteRepository<T>
    {
        Task DeleteAsync(T Id);
    }
}
