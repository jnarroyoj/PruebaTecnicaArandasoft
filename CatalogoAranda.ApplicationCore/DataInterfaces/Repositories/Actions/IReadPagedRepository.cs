using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.DataInterfaces.Repositories.Actions
{
    public interface IReadPagedRepository<T, IdType> where T : class
    {
        Task<IEnumerable<T>> GetManyAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            bool ascendantOrder,
            int page, int recordsPerPage);
        Task<T?> GetAsync(IdType Id);
        Task<int> GetTotalOfRecordsAsync();
        Task<bool> IdNotExistsAsync(IdType Id);
    }
}
