using Application.Interfaces.CategoryInterfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CategoryRepository: ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext 
            ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        var categories =  await _context.Categories.ToListAsync();
        return categories;
    }
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }
    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }


}
