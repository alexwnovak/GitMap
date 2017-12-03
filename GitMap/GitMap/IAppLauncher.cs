namespace GitMap
{
   public interface IAppLauncher
   {
      void LaunchUI();
      void LaunchCommitEditor( string commitFilePath );
   }
}
