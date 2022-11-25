namespace SettingsLibrary.Models;

public interface ISettingsModel
{
   string? SavePath { get; set; }
   string? LastSavePath { get; set; }
}
