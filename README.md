# Settings Library

A simple settings manager that saves settings in a JSON file. It will try to create a new folder in the users AppData folder with the name of the app. Then save the settings file there.

## Usage With WPF

Example Settings Model

```cs
using SettingsLibrary.Models;

public class SettingsModel : ISettingsModel
{
  public string? SavePath { get; set; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Path\to\file.txt";
  public string? LastSavePath { get; set; }
  public int CurrrentTab { get; set; }
}
```

App.xaml.cs

```cs
public partial class App : Application
{
  public static SettingsModel StateSettings { get; private set; } = null!;

  protected override void OnStartup(StartupEventArgs e)
  {
    // Loads the settings from the JSON settings file.
    StateSettings = SettingsManager.OnStartup<SettingsModel>("<AppName>");
    // Other startup operations ...
    base.OnStartup(e);
  }

  protected override void OnExit(ExitEventArgs e)
  {
    // Other shutdown operations ...

    // Saves to the JSON settings file.
    SettingsManager.OnExit(StateSettings, "<AppName>");
    base.OnExit(e);
  }
}
```