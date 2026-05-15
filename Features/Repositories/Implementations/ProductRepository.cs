using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Product>> GetAllAsync()
        => await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Supplier)
            .ToListAsync();

    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
