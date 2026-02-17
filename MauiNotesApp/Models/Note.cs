using SQLite;

namespace MauiNotesApp.Models;

public class Note
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;

    public DateTime LastUpdatedUtc { get; set; } = DateTime.UtcNow;
}
