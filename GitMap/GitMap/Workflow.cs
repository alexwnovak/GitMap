using System;
using GitMap.Core;

namespace GitMap
{
   public class Workflow : IWorkflow
   {
      private readonly string _workflowName;
      private readonly IConfigurationReader _configurationReader;
      private readonly Func<string, string, int> _startProcess;

      public Workflow(
         string workflowName,
         IConfigurationReader configurationReader,
         Func<string, string, int> startProcess )
      {
         _workflowName = workflowName;
         _configurationReader = configurationReader;
         _startProcess = startProcess;
      }

      public int Launch( string parameter )
      {
         var configuration = _configurationReader.Read( _workflowName );

         if ( configuration == EditorConfiguration.Empty )
         {
            return 1;
         }

         string arguments = configuration.Arguments.Replace( "%1", parameter );

         return _startProcess( configuration.FilePath, arguments );
      }
   }
}
