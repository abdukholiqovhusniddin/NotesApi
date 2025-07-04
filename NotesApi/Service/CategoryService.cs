﻿using NotesApi.Exceptions;
using NotesApi.Interfaces.Repository;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;
using NotesAPI.DTOs;

namespace NotesApi.Service;

public class CategoryService(ICategoryRepository categoryRepository, ICurrentUserService current) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICurrentUserService _current = current;

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync() =>
        (await _categoryRepository.GetAllAsync())
            .Select(c => new CategoryDto(c.Id, c.Name, c.UserId));
    public async Task<CategoryDto?> GetCategoryByIdAsync(int id) =>
        (await _categoryRepository.GetByIdAsync(id)) is { } category
            ? new CategoryDto(category.Id, category.Name, _current.UserId)
            : null;

    public async Task<CategoryDto> CreateCategoryAsync(CategoryInputDto createCategoryDto)
    {
        if(await _categoryRepository.GetByNameAsync(createCategoryDto.Name) is not null)
            throw new ApiException("Category with this name already exists.");

        var createdCategory = await _categoryRepository.CreateAsync(new Category { Name = createCategoryDto.Name, UserId = _current.UserId});
        return new CategoryDto(createdCategory.Id, createdCategory.Name, _current.UserId);
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryInputDto updateCategoryDto)
    {
        if(await _categoryRepository.GetByIdAsync(id) is not { } category) return null;

        var existingCategory = await _categoryRepository.GetByNameAsync(updateCategoryDto.Name);
        if (existingCategory != null && existingCategory.Id != id)
            throw new ApiException("Category with this name already exists.");

        category.Name = updateCategoryDto.Name;
        var updatedCategory = await _categoryRepository.UpdateAsync(category);

        return new CategoryDto(updatedCategory.Id, updatedCategory.Name, _current.UserId);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        if(!await _categoryRepository.ExistsAsync(id))  return false;

        if(await _categoryRepository.HasNotesAsync(id))
            throw new ApiException("Cannot delete category that has associated notes.");

        return await _categoryRepository.DeleteAsync(id);
    }
}