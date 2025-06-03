using System.Collections.ObjectModel;

namespace MobileNotesApp;

public partial class MainPage : ContentPage
{
    ObservableCollection<Note> notes = new();

    public MainPage()
    {
        InitializeComponent();

        NotesCollectionView.ItemsSource = notes;
    }

    private async void OnNoteSelected(object sender, SelectionChangedEventArgs e)
    {
        if(e.CurrentSelection.FirstOrDefault() is Note selectedNote)
        {
            await Navigation.PushAsync(new NewNotePage(notes, selectedNote));

            NotesCollectionView.SelectedItem = null; // Deselect after click
        }
    }

    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewNotePage(notes));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        
    }
}
