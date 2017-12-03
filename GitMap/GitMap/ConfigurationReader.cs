using System;
using Microsoft.Win32;

namespace GitMap
{
   public class ConfigurationReader : IConfigurationReader
   {
      public ConfigurationPair Read<T>()
      {
         string workflowName = typeof( T ).Name;

         using ( var key = Registry.CurrentUser.CreateSubKey( @"SOFTWARE\GitMap" ) )
         {
            var filePath = key.GetValue( $"{workflowName}FilePath" );
            var arguments = key.GetValue( $"{workflowName}Arguments" );

            if ( filePath != null && arguments != null )
            {
               return new ConfigurationPair( filePath.ToString(), arguments.ToString() );
            }
         }

         throw new NotImplementedException();
      }
   }
}
