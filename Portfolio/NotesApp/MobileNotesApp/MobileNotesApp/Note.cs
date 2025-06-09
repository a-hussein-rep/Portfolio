using SQLite; 

namespace MobileNotesApp;

public class Note
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }
}
