using System.Linq.Expressions;
using CustomerBalance.Core.Shared;
using CustomerBalance.Core.ViewModels;

namespace CustomerBalance.Server.Repository;

public interface IBaseCrud<T> where T : IEntity
{
    Task<Response<T?>> AddAsync(T entity);
    Task<Response<bool>> UpdateAsync(T entity);
    Task<Response<bool>> DeleteAsync(Guid? id);
    Task<Response<T?>> GetByIdAsync(Guid? id);
    Task<Response<IEnumerable<T?>>> GetAllAsync();
    Task<Response<IEnumerable<T?>>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<Response<T?>> GetAsync(Expression<Func<T, bool>> filter);
    Task<Response<bool>> ExistsAsync(Expression<Func<T, bool>> filter);
}
