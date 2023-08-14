using Domain.Interfaces.Generic;
using Entities.Entities;

namespace Domain.Interfaces.FinancialSystemUsers;

public interface IFinancialSystemUser : IGeneric<FinancialSystemUser>
{
    Task<FinancialSystemUser?> GetFinancialSystemUsersById(int financialSystemId);
    Task RemoveFinancialSystemUsers(List<FinancialSystemUser> financialSystemUsers);
    Task<IList<FinancialSystemUser>> GetFinancialSystemUsersByEmail(string userEmail);
}
