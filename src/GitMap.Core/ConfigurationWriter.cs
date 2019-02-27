﻿using Microsoft.Win32;

namespace GitMap.Core
{
   public static class ConfigurationWriter
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
   }
}
