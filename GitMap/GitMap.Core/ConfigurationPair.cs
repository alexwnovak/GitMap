namespace GitMap.Core
{
   public class ConfigurationPair
   {
      public static readonly ConfigurationPair Empty = new ConfigurationPair();

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

      private ConfigurationPair()
      {
      }
   }
}
