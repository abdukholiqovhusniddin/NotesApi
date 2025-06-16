using Microsoft.AspNetCore.Mvc;
using NotesApi.Interfaces.Sevices;
using NotesAPI.DTOs;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _categoryService = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories() =>
        Ok(await _categoryService.GetAllCategoriesAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id) =>
        await _categoryService.GetCategoryByIdAsync(id) is { } category
            ? Ok(category)
            : NotFound($"Category with ID {id} not found.");
   
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryInputDto createCategoryDto)
    {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryInputDto updateCategoryDto) =>
             await _categoryService.UpdateCategoryAsync(id, updateCategoryDto) is { } updated
                ? Ok(updated)
                : NotFound($"Category with ID {id} not found.");

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id) =>
            await _categoryService.DeleteCategoryAsync(id)
                ? NoContent()
                : NotFound($"Category with ID {id} not found.");
}