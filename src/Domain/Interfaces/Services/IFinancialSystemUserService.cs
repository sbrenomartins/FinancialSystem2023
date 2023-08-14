using Entities.Entities;

namespace Domain.Interfaces.Services;

public interface IFinancialSystemUserService
{
    Task AddFinancialSystemUser(FinancialSystemUser financialSystemUser);
    Task UpdateFinancialSystemUser(FinancialSystemUser financialSystemUser);
}
