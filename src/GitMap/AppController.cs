using System;
using System.Collections.Generic;
using System.IO;

namespace GitMap
{
   public class AppController
   {
      private readonly IDictionary<string, IWorkflow> _workflows;
      private Action _displayBanner;
      private Action<string> _displayConfigurationError;

      public AppController(
         IDictionary<string, IWorkflow> workflows,
         Action displayBanner,
         Action<string> displayConfigurationError )
      {
         _workflows = workflows ?? throw new ArgumentException( nameof( workflows ) );
         _displayBanner = displayBanner;
         _displayConfigurationError = displayConfigurationError;
      }

      private static string GetFilePath( string[] arguments ) =>
         arguments?.Length > 0 ? arguments[0] : string.Empty;

      public int Run( string[] arguments )
      {
         _displayBanner();

         string filePath = GetFilePath( arguments );
         string fileName = Path.GetFileName( filePath );

         if ( _workflows.TryGetValue( fileName, out var workflow ) )
         {
            return workflow.Launch( filePath );
         }

         _displayConfigurationError( fileName );
         return 1;
      }
   }
}
