using JsonReaderLibrary;

using SettingsLibrary.Models;

namespace SettingsLibrary;

public static class SettingsManager
{
   #region Local Props
   private static readonly string _fileName = "settings.json";
   #endregion

   #region Methods
   public static T? OnStartup<T>(string appName) where T : ISettingsModel, new()
   {
      var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName);
      if (Directory.Exists(appDataPath))
      {
         string settingsPath = Path.Combine(appDataPath, _fileName);
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

   public static void OnExit<T>(T settings, string appName) where T : ISettingsModel, new()
   {
      if (string.IsNullOrEmpty(appName)) throw new ArgumentException(nameof(appName), "No name provided. Unable to find settings path.");
      var settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName, _fileName);
      JsonReader.SaveJsonFile(settingsPath, settings, true);
   }
   #endregion

   #region Full Props

   #endregion
}