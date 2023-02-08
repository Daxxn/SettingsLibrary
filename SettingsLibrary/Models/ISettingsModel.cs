namespace SettingsLibrary.Models
{
   /// <summary>
   /// Definition for application settings
   /// </summary>
   public interface ISettingsModel
   {
      /// <summary>
      /// Default save path
      /// </summary>
      string SavePath { get; set; }
      /// <summary>
      /// Previously used save path
      /// </summary>
      string LastSavePath { get; set; }
   }
}