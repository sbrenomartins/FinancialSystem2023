using Domain.Interfaces.Expenses;
using Domain.Interfaces.Services;
using Entities.Entities;

namespace Domain.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpense _expense;

    public ExpenseService(IExpense expense)
    {
        _expense = expense;
    }

    public async Task AddExpense(Expense expense)
    {
        DateTime date = DateTime.UtcNow;
        expense.RegisterDate = date;
        expense.Month = date.Month;
        expense.Year = date.Year;

        bool isValid = expense.ValidateStringProperty(expense.Name, "Name");
        if (isValid)
            await _expense.Add(expense);
    }

    public async Task UpdateExpense(Expense expense)
    {
        DateTime date = DateTime.UtcNow;
        expense.UpdateDate = date;

        if (expense.Paid)
            expense.PaymentDate = date;

        bool isValid = expense.ValidateStringProperty(expense.Name, "Name");
        if (isValid)
            await _expense.Add(expense);
    }
}
