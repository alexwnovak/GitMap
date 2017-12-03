using System;
using System.Collections.Generic;
using System.IO;

namespace GitMap
{
   public class AppController
   {
      private readonly IDictionary<string, IWorkflow> _workflows;

      public AppController( IDictionary<string, IWorkflow> workflows )
      {
         _workflows = workflows ?? throw new ArgumentException( nameof( workflows ) );
      }

      private static string GetFilePath( string[] arguments ) =>
         arguments?.Length > 0 ? arguments[0] : string.Empty;

      public int Run( string[] arguments )
      {
         string filePath = GetFilePath( arguments );
         string fileName = Path.GetFileName( filePath );

         if ( _workflows.TryGetValue( fileName, out var workflow ) )
         {
            workflow.Launch( filePath );
            return 0;
         }

         return 1;
      }
   }
}
