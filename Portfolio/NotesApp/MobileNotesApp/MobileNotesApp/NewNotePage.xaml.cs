using System.Collections.ObjectModel;

using MobileNotesApp.Database;

namespace MobileNotesApp;

partial class NewNotePage : ContentPage
{
    ObservableCollection<Note> notes;

    NotesDatabase database;

    Note note;

    public NewNotePage(ObservableCollection<Note> notes, NotesDatabase notesDatabase, Note toEditNote = null)
    {
        InitializeComponent();

        this.notes = notes;

        this.database = notesDatabase;

        if (toEditNote is not null)
        {
            this.Title = "Edit Note";

            this.note = toEditNote;

            TitleEntry.Text = this.note.Title;
            ContentEditor.Text = this.note.Content;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TitleEntry.Text))
        {
            await DisplayAlert("Error", "Title cannot be empty.", "OK");

            return;
        }

        if (this.note is not null) // note exits, so update it
        {
            this.note.Title = TitleEntry.Text;
            this.note.Content = ContentEditor.Text;
        }

        else // note does not exist, so create it
        {
            this.note = new Note
            {
                Title = TitleEntry.Text,
                Content = ContentEditor.Text
            };

            notes.Add(note);
        }

        await database.SaveNoteAsync(note);

        await Navigation.PopAsync(); // Go back to the main page
    }
}
