using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi.Service;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController(INoteService service) : ControllerBase
{
    private readonly INoteService _service = service;

    // GET: api/notes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _service.GetAllAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var note = await _service.GetByIdAsync(id);
        return note == null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Note note)
    {
        var createdNote = await _service.CreateAsync(note);
        return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Note note)
    {
        var updatedNote = await _service.UpdateAsync(id, note);
        return updatedNote == null ? NotFound() : Ok(updatedNote);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result == null ? NotFound() : NoContent();
    }
}
