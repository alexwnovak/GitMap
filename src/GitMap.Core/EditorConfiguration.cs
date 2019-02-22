namespace GitMap.Core
{
   public class EditorConfiguration
   {
      public static readonly EditorConfiguration Empty = new EditorConfiguration();

      public bool IsEnabled
      {
         get;
         set;
      }

      private string _filePath;
      public string FilePath
      {
         get => _filePath ?? string.Empty;
         set => _filePath = value;
      }

      private string _arguments;
      public string Arguments
      {
         get => _arguments ?? string.Empty;
         set => _arguments = value;
      }
   }
}
