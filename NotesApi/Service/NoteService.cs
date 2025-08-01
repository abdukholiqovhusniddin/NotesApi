﻿using NotesApi.Exceptions;
using NotesApi.Interfaces.Repository;
using NotesApi.Interfaces.Sevices;
using NotesApi.Models;
using NotesAPI.DTOs;

namespace NotesApi.Service;
public class NoteService(INoteRepository noteRepository, ICategoryRepository categoryRepository, ICurrentUserService current) : INoteService
{
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICurrentUserService _current = current;

    public async Task<(IEnumerable<NoteDto> Notes, int TotalCount)> GetAllNotesAsync(
        int? categoryId = null,
        string? search = null,
        int page = 1,
        int pageSize = 10)
    {
        var (notes, totalCount) = await _noteRepository.GetAllAsync(_current.UserId,categoryId, search, page, pageSize);

        return (notes.Select(n => new NoteDto
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            CategoryId = n.CategoryId,
            CategoryName = n.Category.Name,
            CreatedAt = n.CreatedAt,
            UpdatedAt = n.UpdatedAt
        }), totalCount);
    }

    public async Task<NoteDto?> GetNoteByIdAsync(int id) =>
        (await _noteRepository.GetByIdAsync(id, _current.UserId)) is { } note
            ? new NoteDto
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    CategoryId = note.CategoryId,
                    UserId = note.UserId,
                    CategoryName = note.Category.Name,
                    CreatedAt = note.CreatedAt,
                    UpdatedAt = note.UpdatedAt
                }
                : null;

    public async Task<NoteDto> CreateNoteAsync(NoteInputDto createNoteDto)
    {
        if(!await _categoryRepository.ExistsAsync(createNoteDto.CategoryId))
            throw new ApiException("Specified category does not exist.");

        var note = await _noteRepository.CreateAsync(new Note
        {
            Title = createNoteDto.Title,
            Content = createNoteDto.Content,
            CategoryId = createNoteDto.CategoryId,
            UserId = _current.UserId
        });


        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CategoryId = note.CategoryId,
            UserId = note.UserId,
            CategoryName = note.Category.Name,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }

    public async Task<NoteDto?> UpdateNoteAsync(int id, NoteInputDto updateNoteDto)
    {
        var note = await _noteRepository.GetByIdAsync(id, _current.UserId);
        if (note is null) return null;

        if(!await _categoryRepository.ExistsAsync(updateNoteDto.CategoryId))
            throw new ApiException("Specified category does not exist.");

        (note.Title, note.Content, note.CategoryId, note.UserId, note.UpdatedAt) = (updateNoteDto.Title, updateNoteDto.Content, updateNoteDto.CategoryId, _current.UserId, DateTime.UtcNow);

        var updatedNote = await _noteRepository.UpdateAsync(note);

        return new NoteDto
        {
            Id = updatedNote.Id,
            Title = updatedNote.Title,
            Content = updatedNote.Content,
            CategoryId = updatedNote.CategoryId,
            UserId = updatedNote.UserId,
            CategoryName = updatedNote.Category.Name,
            CreatedAt = updatedNote.CreatedAt,
            UpdatedAt = updatedNote.UpdatedAt
        };
    }

    public async Task<bool> DeleteNoteAsync(int id) =>
        await _noteRepository.ExistsAsync(id, _current.UserId) && await _noteRepository.DeleteAsync(id, _current.UserId);
}
