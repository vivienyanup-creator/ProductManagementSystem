using Microsoft.EntityFrameworkCore;
using ProductManagement.Features.Data;
using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;

namespace ProductManagement.Features.Repositories.Implementations;

public class BrandRepository(AppDbContext context) : IBrandRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Brand>> GetAllAsync()
        => await _context.Brands.ToListAsync();

    public async Task<Brand?> GetByIdAsync(int id)
        => await _context.Brands.FindAsync(id);

    public async Task AddAsync(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }
    }
}
