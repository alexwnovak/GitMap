using Microsoft.Win32;
using GitMap.Core;

namespace GitMap
{
   public class ConfigurationReader : IConfigurationReader
   {
      public ConfigurationPair Read( string workflowName )
      {
         ConfigurationPair configuredEditorInfo = ConfigurationPair.Empty;

         using ( var key = Registry.CurrentUser.CreateSubKey( @"SOFTWARE\GitMap" ) )
         {
            var filePath = key.GetValue( $"{workflowName}FilePath" );
            var arguments = key.GetValue( $"{workflowName}Arguments" );

            if ( filePath != null && arguments != null )
            {
               configuredEditorInfo = new ConfigurationPair( filePath.ToString(), arguments.ToString() );
            }
         }

         return configuredEditorInfo;
      }
   }
}
