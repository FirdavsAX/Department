using Application.DTOs.CategoryDto;

namespace Application.Interfaces.CategoryInterfaces;
public interface ICategoryService
{
    Task<IEnumerable<ReadCategoryDto>> GetAllCategoriesAsync();
    Task<ReadCategoryDto> GetCategoryByIdAsync(int id);
    Task<ReadCategoryDto> CreateCategoryAsync(CreateCategoryDto category);
    Task<ReadCategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto category);
    Task<bool> DeleteCategoryAsync(int id);
}
