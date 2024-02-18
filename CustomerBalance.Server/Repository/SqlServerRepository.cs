using System.Linq.Expressions;
using CustomerBalance.Core.Shared;
using CustomerBalance.Core.ViewModels;
using CustomerBalance.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerBalance.Server.Repository;

public class SqlServerRepository<T>(AppDbContext db) : IBaseCrud<T>
    where T : class, IEntity
{
    private readonly AppDbContext _db = db;

    public async Task<Response<T?>> AddAsync(T entity)
    {
        try
        {
            var result = await _db.Set<T>().AddAsync(entity);
            var save = await _db.SaveChangesAsync();

            return new Response<T?>
            {
                Success = save > 0,
                Data = save > 0 ? result.Entity : null,
                Message = save > 0 ? "Record added successfully" : "No record added"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> UpdateAsync(T entity)
    {
        try
        {
            var record = await _db.Set<T>().FindAsync(entity.Id);
            if (record == null)
                return new Response<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "No record matches the ID supplied"
                };

            _db.Entry(record).State = EntityState.Detached;
            _db.Set<T>().Update(entity);
            var save = await _db.SaveChangesAsync();

            return new Response<bool>
            {
                Success = save > 0,
                Data = save > 0,
                Message = save > 0 ? "Record updated successfully" : "No record updated"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> DeleteAsync(Guid? id)
    {
        try
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity == null)
                return new Response<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "No record matches the ID supplied"
                };

            _db.Set<T>().Remove(entity);
            var save = await _db.SaveChangesAsync();

            return new Response<bool>
            {
                Success = save > 0,
                Data = save > 0,
                Message = save > 0 ? "Record deleted successfully" : "No record deleted"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<T?>> GetByIdAsync(Guid? id)
    {
        try
        {
            var result = await _db.Set<T>().FindAsync(id);
            return new Response<T?>
            {
                Success = result != null,
                Data = result,
                Message = result != null ? "Record retrieved successfully" : "No record retrieved"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<IEnumerable<T?>>> GetAllAsync()
    {
        try
        {
            var result = await _db.Set<T>().ToListAsync();
            return new Response<IEnumerable<T?>>
            {
                Success = result.Count > 0,
                Data = result,
                Message = result.Count > 0 ? "Records retrieved successfully" : "No records retrieved"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<IEnumerable<T?>>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            var result = await _db.Set<T>().Where(filter).ToListAsync();
            return new Response<IEnumerable<T?>>
            {
                Success = result.Count > 0,
                Data = result,
                Message = result.Count > 0 ? "Records retrieved successfully" : "No records retrieved"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<T?>> GetAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            var result = await _db.Set<T>().FirstOrDefaultAsync(filter);
            return new Response<T?>
            {
                Success = result != null,
                Data = result,
                Message = result != null ? "Record retrieved successfully" : "No record retrieved"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> ExistsAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            var result = await _db.Set<T>().AnyAsync(filter);
            return new Response<bool>
            {
                Success = result,
                Data = result,
                Message = result ? "Record exists already" : "Record does not exist"
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
