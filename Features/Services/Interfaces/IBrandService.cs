using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces;

public interface IBrandService
{
    Task<List<Brand>> GetBrandsAsync();
    Task<Brand?> GetBrandByIdAsync(int id);
    Task CreateBrandAsync(Brand brand);
    Task UpdateBrandAsync(Brand brand);
    Task DeleteBrandAsync(int id);
}
