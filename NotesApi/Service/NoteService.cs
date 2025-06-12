using NotesApi.Models;
using NotesApi.Repository;
using NotesAPI.DTOs;

namespace NotesApi.Service;
public class NoteService(INoteRepository noteRepository, ICategoryRepository categoryRepository) : INoteService
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<(IEnumerable<NoteDto> Notes, int TotalCount)> GetAllNotesAsync(
        int? categoryId = null,
        string? search = null,
        int page = 1,
        int pageSize = 10)
    {
        var (notes, totalCount) = await _noteRepository.GetAllAsync(categoryId, search, page, pageSize);

        var noteDtos = notes.Select(n => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            CategoryId = n.CategoryId,
            CategoryName = n.Category.Name,
            CreatedAt = n.CreatedAt,
            UpdatedAt = n.UpdatedAt
        });

        return (noteDtos, totalCount);
    }

    public async Task<NoteDto?> GetNoteByIdAsync(int id)
    {
        var note = await _noteRepository.GetByIdAsync(id);
        if (note == null) return null;

        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CategoryId = note.CategoryId,
            CategoryName = note.Category.Name,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }

    public async Task<NoteDto> CreateNoteAsync(NoteInputDto createNoteDto)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(createNoteDto.CategoryId);
        if (!categoryExists)
        {
            throw new InvalidOperationException("Specified category does not exist.");
        }

        var note = new Note
        {
            Title = createNoteDto.Title,
            Content = createNoteDto.Content,
            CategoryId = createNoteDto.CategoryId
        };

        var createdNote = await _noteRepository.CreateAsync(note);

        return new NoteDto
        {
            Id = createdNote.Id,
            Title = createdNote.Title,
            Content = createdNote.Content,
            CategoryId = createdNote.CategoryId,
            CategoryName = createdNote.Category.Name,
            CreatedAt = createdNote.CreatedAt,
            UpdatedAt = createdNote.UpdatedAt
        };
    }

    public async Task<NoteDto?> UpdateNoteAsync(int id, NoteInputDto updateNoteDto)
    {
        var note = await _noteRepository.GetByIdAsync(id);
        if (note == null) return null;

        var categoryExists = await _categoryRepository.ExistsAsync(updateNoteDto.CategoryId);
        if (!categoryExists)
            throw new InvalidOperationException("Specified category does not exist.");

        note.Title = updateNoteDto.Title;
        note.Content = updateNoteDto.Content;
        note.CategoryId = updateNoteDto.CategoryId;

        var updatedNote = await _noteRepository.UpdateAsync(note);

        return new NoteDto
        {
            Id = updatedNote.Id,
            Title = updatedNote.Title,
            Content = updatedNote.Content,
            CategoryId = updatedNote.CategoryId,
            CategoryName = updatedNote.Category.Name,
            CreatedAt = updatedNote.CreatedAt,
            UpdatedAt = updatedNote.UpdatedAt
        };
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var noteExists = await _noteRepository.ExistsAsync(id);
        if (!noteExists) return false;

        return await _noteRepository.DeleteAsync(id);
    }
}
