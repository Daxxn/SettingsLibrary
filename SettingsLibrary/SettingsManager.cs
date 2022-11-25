using JsonReaderLibrary;

using SettingsLibrary.Models;

namespace SettingsLibrary;

public static class SettingsManager
{
   #region Local Props
   private static readonly string FileName = "settings.json";
   public static string? AppName { get; set; }
   #endregion

   #region Methods
   public static T? OnStartup<T>(string appName) where T : ISettingsModel, new()
   {
      AppName = appName;
      var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName);
      if (Directory.Exists(appDataPath))
      {
         string settingsPath = Path.Combine(appDataPath, FileName);
         if (File.Exists(settingsPath))
         {
            return JsonReader.OpenJsonFile<T>(settingsPath);
         }
      }
      else
      {
         Directory.CreateDirectory(appDataPath);
         return OnStartup<T>(appName);
      }
      return new T();
   }

   public static void OnExit<T>(T settings) where T : ISettingsModel, new()
   {
      if (AppName is null) return;
      var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName, FileName);
      JsonReader.SaveJsonFile(settingsPath, settings, true);
   }
   #endregion

   #region Full Props

   #endregion
}