using NotesAPI.DTOs;

namespace NotesApi.Interfaces.Sevices;
public interface INoteService
{
    Task<(IEnumerable<NoteDto> Notes, int TotalCount)> GetAllNotesAsync(
            int UserId,
            int? categoryId = null,
            string? search = null,
            int page = 1,
            int pageSize = 10);
    Task<NoteDto?> GetNoteByIdAsync(int id);
    Task<NoteDto> CreateNoteAsync(NoteInputDto createNoteDto);
    Task<NoteDto?> UpdateNoteAsync(int id, NoteInputDto updateNoteDto);
    Task<bool> DeleteNoteAsync(int id);
}
