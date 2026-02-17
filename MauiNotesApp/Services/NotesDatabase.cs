using MauiNotesApp.Models;
using SQLite;

namespace MauiNotesApp.Services;

public class NotesDatabase
{
    private SQLiteAsyncConnection? _database;

    private async Task InitAsync()
    {
        if (_database is not null)
        {
            return;
        }

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<Note>();
    }

    public async Task<List<Note>> GetNotesAsync()
    {
        await InitAsync();
        return await _database!.Table<Note>()
            .OrderByDescending(x => x.LastUpdatedUtc)
            .ToListAsync();
    }

    public async Task<int> SaveNoteAsync(Note note)
    {
        await InitAsync();
        note.LastUpdatedUtc = DateTime.UtcNow;

        if (note.Id == 0)
        {
            return await _database!.InsertAsync(note);
        }

        return await _database!.UpdateAsync(note);
    }

    public async Task<int> DeleteNoteAsync(Note note)
    {
        await InitAsync();
        return await _database!.DeleteAsync(note);
    }
}
