using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations;

public class SupplierRepository(AppDbContext context) : ISupplierRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Supplier>> GetAllAsync()
        => await _context.Suppliers.ToListAsync();

    public async Task<Supplier?> GetByIdAsync(int id)
        => await _context.Suppliers.FindAsync(id);

    public async Task AddAsync(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
