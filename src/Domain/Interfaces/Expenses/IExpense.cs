using Domain.Interfaces.Generic;
using Entities.Entities;

namespace Domain.Interfaces.Expenses;

public interface IExpense : IGeneric<Expense>
{
    Task<IList<Expense>> GetUserExpensesByEmail(string userEmail);
    Task<IList<Expense>> GetUserNotPaidLastMonthsExpensesByEmail(string userEmail);
}
