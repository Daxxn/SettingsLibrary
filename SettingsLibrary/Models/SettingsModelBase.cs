namespace SettingsLibrary.Models;

/// <summary>
/// Default settings model for application settings
/// </summary>
public class SettingsModelBase : ISettingsModel
{
   #region Props
   /// <summary>
   /// Default save path
   /// </summary>
   public string? SavePath { get; set; }
   /// <summary>
   /// Previously used save path
   /// </summary>
   public string? LastSavePath { get; set; }
   #endregion
}
