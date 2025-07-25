using Application.DTOs.CategoryDto;
using Application.Interfaces.CategoryInterfaces;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository 
            ?? throw new ArgumentNullException(nameof(categoryRepository), "Category repository cannot be null.");
    }
    public async Task<IEnumerable<ReadCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        if (categories == null || !categories.Any())
        {
            return Enumerable.Empty<ReadCategoryDto>();
        }
        var readCategoryDtos = categories.Select(category => new ReadCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        }).ToList();

        return readCategoryDtos;
    }
    public async Task<ReadCategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return null!;
        }
        var readCategoryDto = new ReadCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
        return readCategoryDto;
    }
    public async Task<ReadCategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto category)
    {
        var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category), "Category cannot be null.");
        }
        existingCategory.Name = category.Name ?? existingCategory.Name;
        existingCategory.Description = category.Description ?? existingCategory.Description;

        var updatedCategory = await _categoryRepository.UpdateCategoryAsync(existingCategory);
        if (updatedCategory == null)
        {
            throw new InvalidOperationException("Failed to update category.");
        }
        var readCategoryDto = new ReadCategoryDto
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name,
            Description = updatedCategory.Description
        };
        return readCategoryDto;
    }
    public async Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category), "Category cannot be null.");
        }
        if(string.IsNullOrEmpty(category.Name))
        {
            throw new ValidationException("Category name cannot be null or empty.");
        }
        var newCategory = new Category()
        {
            Name = category.Name,
            Description = category.Description
        };
        var resultNewCategory = await _categoryRepository.CreateCategoryAsync(newCategory);
        if (resultNewCategory == null)
        {
            throw new InvalidOperationException("Failed to create category.");
        }
        var readCategoryDto = new ReadCategoryDto
        {
            Id = newCategory.Id,
            Name = newCategory.Name,
            Description = newCategory.Description
        };
        return readCategoryDto;
    }
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        if(id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Category ID must be greater than zero.");
        }
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }
        var result = await _categoryRepository.DeleteCategoryAsync(id);
        return result;
    }
}
