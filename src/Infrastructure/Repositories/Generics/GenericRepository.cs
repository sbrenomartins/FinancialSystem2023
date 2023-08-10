using System.Runtime.InteropServices;
using Domain.Interfaces.Generic;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace Infrastructure.Repositories.Generics;

public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
{
    private readonly DbContextOptions<BaseContext> _dbContextOptions;

    public GenericRepository()
    {
        _dbContextOptions = new DbContextOptions<BaseContext>();
    }

    public async Task Add(T Object)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            await dbContext.Set<T>().AddAsync(Object);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(T Object)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            dbContext.Set<T>().Remove(Object);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<T>> ReadAll()
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await dbContext.Set<T>().ToListAsync();
        }
    }

    public async Task<T> ReadById(int Id)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await dbContext.Set<T>().FindAsync(Id);
        }
    }

    public async Task Update(T Object)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            dbContext.Set<T>().Update(Object);
            await dbContext.SaveChangesAsync();
        }
    }

    #region Disposed

    bool disposed = false;
    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            handle.Dispose();
        }

        disposed = true;
    }

    #endregion
}
