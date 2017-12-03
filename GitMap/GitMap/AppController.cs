using System.Collections.Generic;
using System.IO;

namespace GitMap
{
   public class AppController
   {
      private readonly IAppLauncher _appLauncher;
      private readonly IDictionary<string, IWorkflow> _workflows;

      public AppController( IAppLauncher appLauncher )
      {
         _appLauncher = appLauncher;
      }

      public AppController( IDictionary<string, IWorkflow> workflows )
      {
         _workflows = workflows;
      }

      public void Run( string[] arguments )
      {
         if ( arguments.Length == 0 )
         {
            _appLauncher.LaunchUI();
         }
         else
         {
            string fileName = Path.GetFileName( arguments[0] );

            if ( _workflows.TryGetValue( fileName, out var workflow ) )
            {
               workflow.Launch( arguments[0] );
            }
         }
      }
   }
}
