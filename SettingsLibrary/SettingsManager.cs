using JsonReaderLibrary;

using SettingsLibrary.Models;

namespace SettingsLibrary;

/// <summary>
/// Manager class for opening and saving application settings.
/// </summary>
public static class SettingsManager
{
   #region Local Props
   private static readonly string _fileName = "settings.json";
   #endregion

   #region Methods
   /// <summary>
   /// Attempt to open the applications settings file from the roaming user folder.
   /// <para/>
   /// Path: <code>%User%\AppData\Roaming\{appName}\settings.json</code>
   /// </summary>
   /// <typeparam name="T">The <see cref="ISettingsModel"/> representation of the applications settings.</typeparam>
   /// <param name="appName">The name of the application</param>
   /// <returns>The settings model or a new settings model.</returns>
   public static T OnStartup<T>(string appName) where T : ISettingsModel, new() =>
      OnStartup<T>(appName, Environment.SpecialFolder.ApplicationData);

   /// <summary>
   /// Attempt to open the applications settings file from the <see cref="Environment.SpecialFolder"/> option.
   /// <para/>
   /// Path: <code>{<paramref name="folder"/>}\{<paramref name="appName"/>}\settings.json</code>
   /// </summary>
   /// <typeparam name="T">The <see cref="ISettingsModel"/> representation of the applications settings.</typeparam>
   /// <param name="appName">The name of the application</param>
   /// <param name="folder">Path to the parent environment folder.</param>
   /// <returns>The settings model or a new settings model.</returns>
   public static T OnStartup<T>(string appName, Environment.SpecialFolder folder) where T : ISettingsModel, new() =>
      OnStartup<T>(appName, Environment.GetFolderPath(folder));

   /// <summary>
   /// Attempt to open the applications settings file from the <see cref="Environment.SpecialFolder"/> option.
   /// <para/>
   /// Path: <code>{<paramref name="folder"/>}\{<paramref name="appName"/>}\settings.json</code>
   /// </summary>
   /// <typeparam name="T">The <see cref="ISettingsModel"/> representation of the applications settings.</typeparam>
   /// <param name="appName">The name of the application</param>
   /// <param name="folder">Path to the desired parent folder.</param>
   /// <returns>The settings model or a new settings model.</returns>
   public static T OnStartup<T>(string appName, string folder) where T : ISettingsModel, new()
   {
      if (!Directory.Exists(folder))
         throw new ArgumentException($"'{folder}' does not exist.", nameof(folder));
      var appDataPath = Path.Combine(folder, appName);
      if (Directory.Exists(appDataPath))
      {
         string settingsPath = Path.Combine(appDataPath, _fileName);
         if (File.Exists(settingsPath))
         {
            var settings = JsonReader.OpenJsonFile<T>(settingsPath);
            if (settings is null)
               return new T();
            else return settings;
         }
      }
      else
      {
         Directory.CreateDirectory(appDataPath);
         return OnStartup<T>(appName);
      }
      return new T();
   }

   /// <summary>
   /// Saves the setting model to the specified folder.
   /// </summary>
   /// <typeparam name="T">The <see cref="ISettingsModel"/> representation of the applications settings.</typeparam>
   /// <param name="settings">Settings model to save</param>
   /// <param name="appName">The name of the application</param>
   /// <exception cref="ArgumentException"></exception>
   public static void OnExit<T>(T settings, string appName) where T : ISettingsModel, new() => OnExit(settings, appName, Environment.SpecialFolder.ApplicationData);

   /// <summary>
   /// Saves the setting model to the specified folder.
   /// </summary>
   /// <typeparam name="T">The <see cref="ISettingsModel"/> representation of the applications settings.</typeparam>
   /// <param name="settings">Settings model to save</param>
   /// <param name="appName">The name of the application</param>
   /// <param name="folder">The environment folder to open</param>
   /// <exception cref="ArgumentException"></exception>
   public static void OnExit<T>(T settings, string appName, Environment.SpecialFolder folder) where T : ISettingsModel, new() =>
      OnExit(settings, appName, Environment.GetFolderPath(folder));

   /// <summary>
   /// Saves the setting model to the specified folder.
   /// </summary>
   /// <typeparam name="T">The <see cref="ISettingsModel"/> representation of the applications settings.</typeparam>
   /// <param name="settings">Settings model to save</param>
   /// <param name="appName">The name of the application</param>
   /// <param name="folder">Path to the desired folder</param>
   /// <exception cref="ArgumentException"></exception>
   public static void OnExit<T>(T settings, string appName, string folder) where T : ISettingsModel, new()
   {
      if (!Directory.Exists(folder))
         throw new ArgumentException($"'{folder ?? "Null"}' does not exist.", nameof(folder));
      if (string.IsNullOrEmpty(appName))
         throw new ArgumentException("No name provided. Unable to find settings path.", nameof(appName));
      var settingsPath = Path.Combine(folder, appName, _fileName);
      JsonReader.SaveJsonFile(settingsPath, settings, true);
   }
   #endregion

   #region Full Props

   #endregion
}