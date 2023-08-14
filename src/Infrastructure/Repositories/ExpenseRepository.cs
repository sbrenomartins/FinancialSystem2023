using Domain.Interfaces.Expenses;
using Entities.Entities;
using Infrastructure.Configurations;
using Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ExpenseRepository : GenericRepository<Expense>, IExpense
{
    private readonly DbContextOptions<BaseContext> _dbContextOptions;

    public ExpenseRepository()
    {
        _dbContextOptions = new DbContextOptions<BaseContext>();
    }

    public async Task<IList<Expense>> GetUserExpensesByEmail(string userEmail)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await
            (from fs in dbContext.FinancialSystems
             join c in dbContext.Categories on fs.Id equals c.SystemId
             join fsu in dbContext.FinancialSystemUsers on fs.Id equals fsu.SystemId
             join e in dbContext.Expenses on c.Id equals e.CategoryId
             where fsu.UserEmail.Equals(userEmail) && fs.Month == e.Month && fs.Year == e.Year
             select e).AsNoTracking().ToListAsync();
        }
    }

    public async Task<IList<Expense>> GetUserNotPaidLastMonthsExpensesByEmail(string userEmail)
    {
        using (var dbContext = new BaseContext(_dbContextOptions))
        {
            return await
            (from fs in dbContext.FinancialSystems
             join c in dbContext.Categories on fs.Id equals c.SystemId
             join fsu in dbContext.FinancialSystemUsers on fs.Id equals fsu.SystemId
             join e in dbContext.Expenses on c.Id equals e.CategoryId
             where fsu.UserEmail.Equals(userEmail) && e.Month < DateTime.Now.Month && !e.Paid
             select e).AsNoTracking().ToListAsync();
        }
    }
}
