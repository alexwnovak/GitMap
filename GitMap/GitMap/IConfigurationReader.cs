using GitMap.Core;

namespace GitMap
{
   public interface IConfigurationReader
   {
      ConfigurationPair Read( string workflowName );
   }
}
