using NotesApi.Models;
using NotesApi.Repository;
using NotesAPI.DTOs;

namespace NotesApi.Service;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryInputDto createCategoryDto)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(createCategoryDto.Name);
        if (existingCategory != null)
            throw new InvalidOperationException("Category with this name already exists.");

        var category = new Category
        {
            Name = createCategoryDto.Name
        };

        var createdCategory = await _categoryRepository.CreateAsync(category);

        return new CategoryDto
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name
        };
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryInputDto updateCategoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        var existingCategory = await _categoryRepository.GetByNameAsync(updateCategoryDto.Name);
        if (existingCategory != null && existingCategory.Id != id)
            throw new InvalidOperationException("Category with this name already exists.");

        category.Name = updateCategoryDto.Name;
        var updatedCategory = await _categoryRepository.UpdateAsync(category);

        return new CategoryDto
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name
        };
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(id);
        if (!categoryExists) return false;

        var hasNotes = await _categoryRepository.HasNotesAsync(id);
        if (hasNotes)
            throw new InvalidOperationException("Cannot delete category that has associated notes.");

        return await _categoryRepository.DeleteAsync(id);
    }
}