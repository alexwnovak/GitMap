using System;
using System.Collections.Generic;
using System.IO;

namespace GitMap
{
   public class AppController
   {
      private readonly IDictionary<string, IWorkflow> _workflows;
      private readonly IOutputController _outputController;

      public AppController( IDictionary<string, IWorkflow> workflows, IOutputController outputController )
      {
         _workflows = workflows ?? throw new ArgumentException( nameof( workflows ) );
         _outputController = outputController;
      }

      private static string GetFilePath( string[] arguments ) =>
         arguments?.Length > 0 ? arguments[0] : string.Empty;

      public int Run( string[] arguments )
      {
         _outputController.DisplayBanner();

         string filePath = GetFilePath( arguments );
         string fileName = Path.GetFileName( filePath );

         if ( _workflows.TryGetValue( fileName, out var workflow ) )
         {
            return workflow.Launch( filePath );
         }

         return 1;
      }
   }
}
