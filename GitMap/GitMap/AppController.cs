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
         _appLauncher.LaunchUI();
      }
   }
}
