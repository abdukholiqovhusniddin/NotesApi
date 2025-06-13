namespace NotesApi.Exceptions;

public class NotFoundException(string message) : Exception(message + " not found");