using System.Diagnostics;

namespace GitMap
{
   public class ProcessRunner : IProcessRunner
   {
      public void Run( string fileName, string arguments )
      {
         var process = Process.Start( fileName, arguments );

         process.WaitForExit();
      }
   }
}
