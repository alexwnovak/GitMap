using System;
using System.IO;

namespace GitMap
{
   public class ConfigurationWorkflow : IWorkflow
   {
      private readonly Func<string, string, int> _startProcess;

      public ConfigurationWorkflow( Func<string, string, int> startProcess )
      {
         _startProcess = startProcess ?? throw new ArgumentNullException( nameof( startProcess ) );
      }

      public int Launch( string parameter )
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
         string configurationExe = Path.Combine( baseDirectory, "GitMap.ConfigurationUI.exe" );
         return _startProcess( configurationExe, null );
      }
   }
}
