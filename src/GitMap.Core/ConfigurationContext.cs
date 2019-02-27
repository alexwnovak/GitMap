using System.ComponentModel;
using Microsoft.Win32;

namespace GitMap.Core
{
   public static class ConfigurationContext
   {
      public static void Write( string workflowName, EditorConfiguration editorConfiguration )
      {
         using ( var key = Registry.CurrentUser.CreateSubKey( @"SOFTWARE\GitMap" ) )
         {
            key.SetValue( $"{workflowName}IsEnabled", editorConfiguration.IsEnabled );
            key.SetValue( $"{workflowName}FilePath", editorConfiguration.FilePath );
            key.SetValue( $"{workflowName}Arguments", editorConfiguration.Arguments );
         }
      }

      public static EditorConfiguration Read( string workflowName )
      {
         using ( var key = Registry.CurrentUser.CreateSubKey( @"SOFTWARE\GitMap" ) )
         {
            return new EditorConfiguration
            {
               IsEnabled = ReadValue<bool>( key, $"{workflowName}IsEnabled" ),
               FilePath = ReadValue<string>( key, $"{workflowName}FilePath" ),
               Arguments = ReadValue<string>( key, $"{workflowName}Arguments" )
            };
         }
      }

      private static T ReadValue<T>( RegistryKey key, string name )
      {
         var value = key.GetValue( name );

         if ( value == null )
         {
            return default( T );
         }

         var typeConverter = TypeDescriptor.GetConverter( typeof( T ) );
         return (T) typeConverter.ConvertFromString( value.ToString() );
      }
   }
}
