using Microsoft.Win32;

namespace GitMap.ConfigurationUI.Services
{
   public static class FileBrowserService
   {
      public static string PickSingleFile()
      {
         var openFileDialog = new OpenFileDialog
         {
            Filter = "Executable files|*.exe;*.bat",
            Title = "Choose Editor"
         };
         
         var result = openFileDialog.ShowDialog();

         return result.HasValue && result.Value ? openFileDialog.FileName : null;
      }
   }
}
