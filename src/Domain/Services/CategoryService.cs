using Domain.Interfaces.Categories;
using Domain.Interfaces.Services;
using Entities.Entities;

namespace Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategory _category;

    public CategoryService(ICategory category)
    {
        _category = category;
    }

    public async Task AddCategory(Category category)
    {
        bool isValid = category.ValidateStringProperty(category.Name, "Name");
        if (isValid)
            await _category.Add(category);
    }

    public async Task UpdateCategory(Category category)
    {
        bool isValid = category.ValidateStringProperty(category.Name, "Name");
        if (isValid)
            await _category.Update(category);
    }
}
