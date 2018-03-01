using Microsoft.Win32;

namespace GitMap.ConfigurationUI.Services
{
   public class FileBrowserService : IFileBrowserService
   {
      public string PickSingleFile()
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
