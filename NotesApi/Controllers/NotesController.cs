using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Interfaces.Sevices;
using NotesAPI.DTOs;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotesController(INoteService service) : ControllerBase
{
    private readonly INoteService _noteService = service;

    [HttpGet]
    public async Task<ActionResult<object>> GetNotes(
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

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetNote(int id) =>
        (await _noteService.GetNoteByIdAsync(id)) is { } note
            ? Ok(note) : NotFound($"Note with ID {id} not found.");

    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote(NoteInputDto createNoteDto)
    {
        var note = await _noteService.CreateNoteAsync(createNoteDto);
        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NoteDto>> UpdateNote(int id, NoteInputDto updateNoteDto) =>
                await _noteService.UpdateNoteAsync(id, updateNoteDto) is { } updatedNote
                ? Ok(updatedNote)
                : NotFound($"Note with ID {id} not found.");

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id) =>
        await _noteService.DeleteNoteAsync(id)
            ? NoContent() : NotFound($"Note with ID {id} not found.");
}
