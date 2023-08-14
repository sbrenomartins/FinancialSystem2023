using Domain.Interfaces.FinancialSystemUsers;
using Entities.Entities;
using Infrastructure.Configurations;
using Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class FinancialSystemUserRepository : GenericRepository<FinancialSystemUser>, IFinancialSystemUser
{
    private readonly DbContextOptions<BaseContext> _dbContextOptions;

    public FinancialSystemUserRepository()
    {
        _dbContextOptions = new DbContextOptions<BaseContext>();
    }

    public async Task<IList<FinancialSystemUser>> GetFinancialSystemUsersByEmail(string userEmail)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await dbContext.FinancialSystemUsers
                .Where(fsu => fsu.UserEmail == userEmail)
                .AsNoTracking()
                .ToListAsync();
        }
    }

    public async Task<FinancialSystemUser?> GetFinancialSystemUsersById(int financialSystemId)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await dbContext.FinancialSystemUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(fsu => fsu.SystemId == financialSystemId);
        }
    }

    public async Task RemoveFinancialSystemUsers(List<FinancialSystemUser> financialSystemUsers)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            dbContext.FinancialSystemUsers
                .RemoveRange(financialSystemUsers);
            await dbContext.SaveChangesAsync();
        }
    }
}
