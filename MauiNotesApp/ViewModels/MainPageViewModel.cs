using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiNotesApp.Models;
using MauiNotesApp.Services;
using System.Windows.Input;

namespace MauiNotesApp.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly NotesDatabase _notesDatabase;
    private string _title = string.Empty;
    private string _content = string.Empty;
    private Note? _selectedNote;

    public ObservableCollection<Note> Notes { get; } = [];

    public string TitleText
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string ContentText
    {
        get => _content;
        set
        {
            _content = value;
            OnPropertyChanged();
        }
    }

    public Note? SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            OnPropertyChanged();

            if (value is not null)
            {
                TitleText = value.Title;
                ContentText = value.Content;
            }
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand NewCommand { get; }

    public MainPageViewModel(NotesDatabase notesDatabase)
    {
        _notesDatabase = notesDatabase;

        LoadCommand = new Command(async () => await LoadNotesAsync());
        SaveCommand = new Command(async () => await SaveAsync());
        DeleteCommand = new Command(async () => await DeleteAsync());
        NewCommand = new Command(ClearForm);
    }

    public async Task LoadNotesAsync()
    {
        var notes = await _notesDatabase.GetNotesAsync();
        Notes.Clear();

        foreach (var note in notes)
        {
            Notes.Add(note);
        }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(TitleText) && string.IsNullOrWhiteSpace(ContentText))
        {
            return;
        }

        var note = SelectedNote ?? new Note();
        note.Title = TitleText.Trim();
        note.Content = ContentText.Trim();

        await _notesDatabase.SaveNoteAsync(note);
        await LoadNotesAsync();
        SelectedNote = note;
    }

    private async Task DeleteAsync()
    {
        if (SelectedNote is null)
        {
            return;
        }

        await _notesDatabase.DeleteNoteAsync(SelectedNote);
        await LoadNotesAsync();
        ClearForm();
    }

    private void ClearForm()
    {
        SelectedNote = null;
        TitleText = string.Empty;
        ContentText = string.Empty;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
