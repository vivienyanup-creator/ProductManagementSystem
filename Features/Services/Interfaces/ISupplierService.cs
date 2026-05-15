using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces;

public interface ISupplierService
{
    Task<List<Supplier>> GetSuppliersAsync();
    Task<Supplier?> GetSupplierByIdAsync(int id);
    Task CreateSupplierAsync(Supplier supplier);
    Task UpdateSupplierAsync(Supplier supplier);
    Task DeleteSupplierAsync(int id);
}
