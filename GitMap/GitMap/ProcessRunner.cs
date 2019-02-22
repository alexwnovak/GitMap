using System.Diagnostics;

namespace GitMap
{
   public static class ProcessRunner
   {
      public static int Run( string fileName, string arguments )
      {
         using ( var process = Process.Start( fileName, arguments ) )
         {
            process.WaitForExit();

            return process.ExitCode;
         }
      }
   }
}
