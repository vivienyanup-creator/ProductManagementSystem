using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations;

public class CategoryService(ICategoryRepository repo) : ICategoryService
{
    private readonly ICategoryRepository _repo = repo;

    public Task<List<Category>> GetCategoriesAsync()
        => _repo.GetAllAsync();

    public Task<Category?> GetCategoryByIdAsync(int id)
        => _repo.GetByIdAsync(id);

    public Task CreateCategoryAsync(Category category)
        => _repo.AddAsync(category);

    public Task UpdateCategoryAsync(Category category)
        => _repo.UpdateAsync(category);

    public Task DeleteCategoryAsync(int id)
        => _repo.DeleteAsync(id);
}
