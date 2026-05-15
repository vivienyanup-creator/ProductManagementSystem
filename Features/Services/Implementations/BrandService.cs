using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations;

public class BrandService(IBrandRepository repo) : IBrandService
{
    private readonly IBrandRepository _repo = repo;

    public Task<List<Brand>> GetBrandsAsync()
        => _repo.GetAllAsync();

    public Task<Brand?> GetBrandByIdAsync(int id)
        => _repo.GetByIdAsync(id);

    public Task CreateBrandAsync(Brand brand)
        => _repo.AddAsync(brand);

    public Task UpdateBrandAsync(Brand brand)
        => _repo.UpdateAsync(brand);

    public Task DeleteBrandAsync(int id)
        => _repo.DeleteAsync(id);
}
