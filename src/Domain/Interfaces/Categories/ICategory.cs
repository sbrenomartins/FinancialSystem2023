using Domain.Interfaces.Generic;
using Entities.Entities;

namespace Domain.Interfaces.Categories;

public interface ICategory : IGeneric<Category>
{
    Task<IList<Category>> GetUserCategoriesByEmail(string userEmail);
}
