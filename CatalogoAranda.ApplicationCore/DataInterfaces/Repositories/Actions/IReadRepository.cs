using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions
{
    public interface IReadRepository<T, IdType> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(IdType Id);
        Task<bool> IdNotExistsAsync(IdType Id);
    }
}
