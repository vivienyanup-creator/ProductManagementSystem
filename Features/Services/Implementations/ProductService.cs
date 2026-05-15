using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations;

public class ProductService(IProductRepository repo) : IProductService
{
    private readonly IProductRepository _repo = repo;

    public Task<List<Product>> GetProductsAsync()
        => _repo.GetAllAsync();

    public Task<Product?> GetProductByIdAsync(int id)
        => _repo.GetByIdAsync(id);

    public Task CreateProductAsync(Product product)
        => _repo.AddAsync(product);

    public Task UpdateProductAsync(Product product)
        => _repo.UpdateAsync(product);

    public Task DeleteProductAsync(int id)
        => _repo.DeleteAsync(id);
}
