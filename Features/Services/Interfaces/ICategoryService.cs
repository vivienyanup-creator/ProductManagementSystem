using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
}
