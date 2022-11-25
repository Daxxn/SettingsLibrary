namespace SettingsLibrary.Models;

public class SettingsModelBase : ISettingsModel
{
   #region Props
   public string? SavePath { get; set; }
   public string? LastSavePath { get; set; }
   #endregion
}
