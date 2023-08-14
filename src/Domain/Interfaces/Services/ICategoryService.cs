using Entities.Entities;

namespace Domain.Interfaces.Services;

public interface ICategoryService
{
    Task AddCategory(Category category);
    Task UpdateCategory(Category category);
}
