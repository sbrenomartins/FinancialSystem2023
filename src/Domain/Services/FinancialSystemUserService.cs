using Domain.Interfaces.FinancialSystemUsers;
using Domain.Interfaces.Services;
using Entities.Entities;

namespace Domain.Services;

public class FinancialSystemUserService : IFinancialSystemUserService
{
    private readonly IFinancialSystemUser _financialSystemUser;

    public FinancialSystemUserService(IFinancialSystemUser financialSystemUser)
    {
        _financialSystemUser = financialSystemUser;
    }

    public async Task AddFinancialSystemUser(FinancialSystemUser financialSystemUser)
    {
        await _financialSystemUser.Add(financialSystemUser);
    }

    public async Task UpdateFinancialSystemUser(FinancialSystemUser financialSystemUser)
    {
        await _financialSystemUser.Update(financialSystemUser);
    }
}
