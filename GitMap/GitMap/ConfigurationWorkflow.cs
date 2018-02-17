using System;
using System.IO;

namespace GitMap
{
   public class ConfigurationWorkflow : IWorkflow
   {
      private readonly IProcessRunner _processRunner;

      public ConfigurationWorkflow( IProcessRunner processRunner )
      {
         _processRunner = processRunner ?? throw new ArgumentNullException( nameof( processRunner ) );
      }

      public int Launch( string parameter )
      {
         string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
         string configurationExe = Path.Combine( baseDirectory, "GitMap.ConfigurationUI.exe" );
         return _processRunner.Run( configurationExe, null );
      }
   }
}
