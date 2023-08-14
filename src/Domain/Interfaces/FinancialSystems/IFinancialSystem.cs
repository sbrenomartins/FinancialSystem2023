using Domain.Interfaces.Generic;
using Entities.Entities;

namespace Domain.Interfaces.FinancialSystems;

public interface IFinancialSystem : IGeneric<FinancialSystem>
{
    Task<IList<FinancialSystem>> GetUserFinancialSystemsByEmail(string userEmail);
}
