using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;
using NotesAPI.DTOs;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotesController(INoteService service) : ControllerBase
{
    private readonly INoteService _noteService = service;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<object>>> GetNotes(
        [FromQuery] int? categoryId = null,
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        page = Math.Max(page, 1);
        if (pageSize < 1 || pageSize > 100) pageSize = 10;

        var (notes, totalCount) = await _noteService.GetAllNotesAsync(categoryId, search, page, pageSize);

        var response = new 
        {
            Notes = notes,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        return Ok(new ApiResponse<object>
        {
            Data = response,
            StatusCode = 200
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetNote(int id) =>
        (await _noteService.GetNoteByIdAsync(id)) is { } note
            ? Ok(new ApiResponse<object>
            {
                Data = note,
                StatusCode = 200
            }) : NotFound((new ApiResponse<object>
            {
                Error = $"Note with ID {id} not found.",
                StatusCode = 404
            }));

    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote(NoteInputDto createNoteDto)
    {
        var note = await _noteService.CreateNoteAsync(createNoteDto);
        return Ok((new ApiResponse<object>
        {
            Data = note,
            StatusCode = 200
        }));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NoteDto>> UpdateNote(int id, NoteInputDto updateNoteDto) =>
                await _noteService.UpdateNoteAsync(id, updateNoteDto) is { } updatedNote
                ? Ok(new ApiResponse<object>
                {
                    Data = updatedNote,
                    StatusCode = 200
                })
                : NotFound(new ApiResponse<object>
                {
                    Error = $"Note with ID {id} not found.",
                    StatusCode = 404
                });

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id) =>
        await _noteService.DeleteNoteAsync(id)
            ? NoContent() : NotFound((new ApiResponse<object>
            {
                Error = $"Note with ID {id} not found.",
                StatusCode = 404
            }));
}
