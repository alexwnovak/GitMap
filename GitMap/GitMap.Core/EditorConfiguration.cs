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

      public string FilePath
      {
         get;
         set;
      }

      public string Arguments
      {
         get;
         set;
      }
   }
}
