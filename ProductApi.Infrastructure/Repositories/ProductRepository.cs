using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Persistence;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
    }
}
