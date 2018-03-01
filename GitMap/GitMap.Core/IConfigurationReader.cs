namespace GitMap.Core
{
   public interface IConfigurationReader
   {
      ConfigurationPair Read( string workflowName );
   }
}
