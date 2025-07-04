using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;
using NotesAPI.DTOs;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _categoryService = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories() =>
        Ok(new ApiResponse<object>
        {
            Data = await _categoryService.GetAllCategoriesAsync(),
            StatusCode = 200
        });

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id) =>
        await _categoryService.GetCategoryByIdAsync(id) is { } category
            ? Ok((new ApiResponse<object>
            {
                Data = category,
                StatusCode = 200
            }))
            : NotFound(new ApiResponse<object>
            {
                Error = $"Category with ID {id} not found.",
                StatusCode = 404
            });
   
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryInputDto createCategoryDto)
    {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok(new ApiResponse<object>
            {
                Data = category,
                StatusCode = 201
            });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryInputDto updateCategoryDto) =>
             await _categoryService.UpdateCategoryAsync(id, updateCategoryDto) is { } updated
                ? Ok((new ApiResponse<object>
            {
                Data = updated,
                StatusCode = 200
            }))
                : NotFound((new ApiResponse<object>
                {
                    Error = $"Category with ID {id} not found.",
                    StatusCode = 404
                }));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id) =>
            await _categoryService.DeleteCategoryAsync(id)
                ? NoContent()
                : NotFound((new ApiResponse<object>
                {
                    Error = $"Category with ID {id} not found.",
                    StatusCode = 404
                }));
}