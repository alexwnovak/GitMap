using System;
using System.Collections.Generic;
using System.IO;

namespace GitMap
{
   public class AppController
   {
      private readonly Func<IDictionary<string, IWorkflow>> _getWorkflows;
      private readonly Action _displayBanner;
      private readonly Action<string> _displayConfigurationError;

      public AppController(
         Func<IDictionary<string, IWorkflow>> getWorkflows,
         Action displayBanner,
         Action<string> displayConfigurationError )
      {
         _getWorkflows = getWorkflows;
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

         var workflows = _getWorkflows();

         if ( workflows.TryGetValue( fileName, out var workflow ) )
         {
            return workflow.Launch( filePath );
         }

         _displayConfigurationError( fileName );
         return 1;
      }
   }
}
