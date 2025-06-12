using Microsoft.AspNetCore.Mvc;
using NotesApi.Service;
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
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound($"Category with ID {id} not found.");
        
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryInputDto createCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest("Error in CreateCategory. \n" + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryInputDto updateCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var category = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return Ok(category);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest("Error in UpdateCategory. " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound($"Category with ID {id} not found.");

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest("Error in DeleteCategory" + ex.Message);
        }
    }
}