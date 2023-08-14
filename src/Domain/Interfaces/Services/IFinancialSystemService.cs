using Entities.Entities;

namespace Domain.Interfaces.Services;

public interface IFinancialSystemService
{
    Task AddFinancialSystem(FinancialSystem financialSystem);
    Task UpdateFinancialSystem(FinancialSystem financialSystem);
}
