using Domain.Interfaces.FinancialSystems;
using Entities.Entities;
using Infrastructure.Configurations;
using Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class FinancialSystemRepository : GenericRepository<FinancialSystem>, IFinancialSystem
{
    private readonly DbContextOptions<BaseContext> _dbContextOptions;

    public FinancialSystemRepository()
    {
        _dbContextOptions = new DbContextOptions<BaseContext>();
    }

    public async Task<IList<FinancialSystem>> GetUserFinancialSystemsByEmail(string userEmail)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await
            (from fs in dbContext.FinancialSystems
             join fsu in dbContext.FinancialSystemUsers on fs.Id equals fsu.SystemId
             where fsu.UserEmail.Equals(userEmail)
             select fs).AsNoTracking().ToListAsync();
        }
    }
}
