using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations;

public class SupplierService(ISupplierRepository repo) : ISupplierService
{
    private readonly ISupplierRepository _repo = repo;

    public Task<List<Supplier>> GetSuppliersAsync()
        => _repo.GetAllAsync();

    public Task<Supplier?> GetSupplierByIdAsync(int id)
        => _repo.GetByIdAsync(id);

    public Task CreateSupplierAsync(Supplier supplier)
        => _repo.AddAsync(supplier);

    public Task UpdateSupplierAsync(Supplier supplier)
        => _repo.UpdateAsync(supplier);

    public Task DeleteSupplierAsync(int id)
        => _repo.DeleteAsync(id);
}
