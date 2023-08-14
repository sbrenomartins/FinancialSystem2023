using Domain.Interfaces.FinancialSystems;
using Domain.Interfaces.Services;
using Entities.Entities;

namespace Domain.Services;

public class FinancialSystemService : IFinancialSystemService
{
    private readonly IFinancialSystem _financialSystem;

    public FinancialSystemService(IFinancialSystem financialSystem)
    {
        _financialSystem = financialSystem;
    }

    public async Task AddFinancialSystem(FinancialSystem financialSystem)
    {
        bool isValid = financialSystem.ValidateStringProperty(financialSystem.Name, "Name");
        if (isValid)
        {
            DateTime date = DateTime.UtcNow;

            financialSystem.CloseDay = 1;
            financialSystem.Year = date.Year;
            financialSystem.Month = date.Month;
            financialSystem.YearCopy = date.Year;
            financialSystem.MonthCopy = date.Month;
            financialSystem.MakeExpenseCopy = true;

            await _financialSystem.Add(financialSystem);
        }
    }

    public async Task UpdateFinancialSystem(FinancialSystem financialSystem)
    {
        bool isValid = financialSystem.ValidateStringProperty(financialSystem.Name, "Name");
        if (isValid)
        {
            financialSystem.CloseDay = 1;
            await _financialSystem.Add(financialSystem);
        }
    }
}
