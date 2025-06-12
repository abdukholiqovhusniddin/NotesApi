using NotesAPI.DTOs;

namespace NotesApi.Service;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CategoryInputDto createCategoryDto);
    Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryInputDto updateCategoryDto);
    Task<bool> DeleteCategoryAsync(int id);
}