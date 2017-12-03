namespace GitMap
{
   public class ConfigurationPair
   {
      public string FilePath
      {
         get;
      }

      public string Arguments
      {
         get;
      }

      public ConfigurationPair( string filePath, string arguments )
      {
         FilePath = filePath;
         Arguments = arguments;
      }
   }
}
