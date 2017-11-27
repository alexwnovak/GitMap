using System.IO;
using GitModel;

namespace GitMap
{
   public class AppController
   {
      private readonly IAppLauncher _appLauncher;

      public AppController( IAppLauncher appLauncher )
      {
         _appLauncher = appLauncher;
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

            if ( fileName == GitFileNames.CommitFileName )
            {
               _appLauncher.LaunchCommitEditor( arguments[0] );
            }
         }
      }
   }
}
