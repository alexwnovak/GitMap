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
         if ( arguments[0] == GitFileNames.CommitFileName )
         {
            _appLauncher.LaunchCommitEditor( arguments[0] );
         }

         _appLauncher.LaunchUI();
      }
   }
}
