using System.Collections.ObjectModel;

namespace MobileNotesApp;

partial class NewNotePage : ContentPage
{
    ObservableCollection<Note> _notes;
    Note _toEditNote;

    public NewNotePage(ObservableCollection<Note> notes, Note toEditNote = null)
    {
        InitializeComponent();

        _notes = notes;
        _toEditNote = toEditNote;

        if (toEditNote is not null)
        {
            TitleEntry.Text = _toEditNote.Title;
            ContentEditor.Text = _toEditNote.Content;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(TitleEntry.Text))
        {
            await DisplayAlert("Error", "Title cannot be empty.", "OK");
            return;
        }

        if(_toEditNote is not null) // note exits, so update it
        {
            _toEditNote.Title = TitleEntry.Text;
            _toEditNote.Content = ContentEditor.Text;
        }

        else // note does not exist, so create it
        {
            var note = new Note
            {
                Title = TitleEntry.Text,
                Content = ContentEditor.Text
            };

            _notes.Add(note); 
        }

        await Navigation.PopAsync(); // Go back to the main page
    }
}
