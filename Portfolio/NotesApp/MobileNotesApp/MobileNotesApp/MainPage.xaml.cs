using System.Collections.ObjectModel;

using MobileNotesApp.Database;

namespace MobileNotesApp;

public partial class MainPage : ContentPage
{
    ObservableCollection<Note> notes = new();

    NotesDatabase database;

    public MainPage(NotesDatabase notesDatabase)
    {
        InitializeComponent();

        database = notesDatabase;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadNotesAsync();
    }

    private async Task LoadNotesAsync()
    {
        var notesFromDb = await database.GetNotesAsync();

        this.notes = new ObservableCollection<Note>(notesFromDb);
        
        NotesCollectionView.ItemsSource = this.notes;
    }

    private async void OnNoteSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Note selectedNote)
        {
            await Navigation.PushAsync(new NewNotePage(notes,database, selectedNote));

            NotesCollectionView.SelectedItem = null;
        }
    }

    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewNotePage(notes, database));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Delete button clicked");

        var button = sender as Button;
        var noteToDelete = button?.CommandParameter as Note;

        if (noteToDelete is null)
            return;

        bool confirm = await DisplayAlert(
            "Delete Note",
            $"Are you sure you want to delete \"{noteToDelete.Title}\"?",
            "Yes", "No");


        if (confirm)
        {
            await database.DeleteNoteAsync(noteToDelete);

            notes.Remove(noteToDelete);
        }
    }
}
