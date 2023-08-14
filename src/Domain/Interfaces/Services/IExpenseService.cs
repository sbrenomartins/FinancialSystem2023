using Entities.Entities;

namespace Domain.Interfaces.Services;

public interface IExpenseService
{
    Task AddExpense(Expense expense);
    Task UpdateExpense(Expense expense);
}
