namespace GitMap.Core
{
   public class ConfigurationPair
   {
      public static readonly ConfigurationPair Empty = new ConfigurationPair();

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
