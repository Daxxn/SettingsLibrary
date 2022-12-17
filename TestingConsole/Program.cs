using KiCadSnippetManager.Models;

using SettingsLibrary;

namespace TestingConsole
{
   internal class Program
   {
      static void Main(string[] args)
      {
         var settings = SettingsManager.OnStartup<SettingsModel>("KiCadSnippetManager");
         Console.WriteLine(settings.SavePath);
      }
   }
}