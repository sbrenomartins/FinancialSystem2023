using Domain.Interfaces.Categories;
using Entities.Entities;
using Infrastructure.Configurations;
using Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategory
{
    private readonly DbContextOptions<BaseContext> _dbContextOptions;

    public CategoryRepository()
    {
        _dbContextOptions = new DbContextOptions<BaseContext>();
    }

    public async Task<IList<Category>> GetUserCategoriesByEmail(string userEmail)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await
            (from fs in dbContext.FinancialSystems
             join c in dbContext.Categories on fs.Id equals c.SystemId
             join fsu in dbContext.FinancialSystemUsers on fs.Id equals fsu.SystemId
             where fsu.UserEmail.Equals(userEmail) && fsu.ActualSystem
             select c).AsNoTracking().ToListAsync();
        }
    }
}
