# Maui Notes App (SQLite)

A simple .NET MAUI notes app using local SQLite storage.

## Features
- Create, edit, and delete notes.
- Persist notes locally using `sqlite-net-pcl`.
- Simple MVVM structure with a single-page UI.

## Project structure
- `Models/Note.cs` – note entity.
- `Services/NotesDatabase.cs` – SQLite CRUD service.
- `ViewModels/MainPageViewModel.cs` – screen state + commands.
- `Views/MainPage.xaml` – notes UI.

## Build prerequisites

2. Install MAUI workloads:
   ```bash
   dotnet workload install maui
   ```

## Run
```bash
dotnet build MauiNotes-App-SQLite.sln
```
