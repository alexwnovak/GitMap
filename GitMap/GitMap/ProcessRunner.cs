using System.Diagnostics;

namespace GitMap
{
   public class ProcessRunner : IProcessRunner
   {
      public int RunOld( string fileName, string arguments )
      {
         using ( var process = Process.Start( fileName, arguments ) )
         {
            process.WaitForExit();

            return process.ExitCode;
         }
      }
   }
}
