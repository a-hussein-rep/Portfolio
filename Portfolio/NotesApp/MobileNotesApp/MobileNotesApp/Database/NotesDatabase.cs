using SQLite;

namespace MobileNotesApp.Database;

public class NotesDatabase
{
    private readonly SQLiteAsyncConnection database;

    public NotesDatabase(string dbPath)
    {
        database = new SQLiteAsyncConnection(dbPath);
        database.CreateTableAsync<Note>().Wait(); // Ensure the table is created
    }

    public async Task<List<Note>> GetNotesAsync()
    {
        var notes = await database.Table<Note>().ToListAsync() ?? [];

        return notes;
    }

    public async Task<int> SaveNoteAsync(Note note)
    {
        if (note is null)
        {
            throw new ArgumentNullException(nameof(note), "Note cannot be null.");
        }

        if (note.Id != 0)
        {
            return await database.UpdateAsync(note);
        }

        return await database.InsertAsync(note);
    }

    public async Task<int> DeleteNoteAsync(Note note)
    {
        if (note is null)
        {
            throw new ArgumentNullException(nameof(note), "Note cannot be null.");
        }

        return await database.DeleteAsync(note);
    }
}
