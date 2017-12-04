using System.Diagnostics;

namespace GitMap
{
   public class ProcessRunner : IProcessRunner
   {
      public int Run( string fileName, string arguments )
      {
         var process = Process.Start( fileName, arguments );

         process.WaitForExit();

         return process.ExitCode;
      }
   }
}
