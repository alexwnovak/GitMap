﻿using System.ComponentModel;
using Microsoft.Win32;

namespace GitMap.Core
{
   public class ConfigurationReader : IConfigurationReader
   {
      public ConfigurationPair Read( string workflowName )
      {
         using ( var key = Registry.CurrentUser.CreateSubKey( @"SOFTWARE\GitMap" ) )
         {
            return new ConfigurationPair
            {
               IsEnabled = ReadValue<bool>( key, $"{workflowName}IsEnabled" ),
               FilePath = ReadValue<string>( key, $"{workflowName}FilePath" ),
               Arguments = ReadValue<string>( key, $"{workflowName}Arguments" )
            };
         }
      }

      private T ReadValue<T>( RegistryKey key, string name )
      {
         var value = key.GetValue( name );

         if ( value == null )
         {
            return default( T );
         }

         var typeConverter = TypeDescriptor.GetConverter( typeof( T ) );
         return (T) typeConverter.ConvertTo( value, typeof( T ) );
      }
   }
}
