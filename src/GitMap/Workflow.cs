using System;
using GitMap.Core;

namespace GitMap
{
   public class Workflow : IWorkflow
   {
      private readonly string _workflowName;
      private readonly Func<string, EditorConfiguration> _readConfiguration;
      private readonly Func<string, string, int> _startProcess;

      public Workflow(
         string workflowName,
         Func<string, EditorConfiguration> readConfiguration,
         Func<string, string, int> startProcess )
      {
         _workflowName = workflowName;
         _readConfiguration = readConfiguration;
         _startProcess = startProcess;
      }

      public int Launch( string parameter )
      {
         var configuration = _readConfiguration( _workflowName );

         if ( configuration == EditorConfiguration.Empty )
         {
            return 1;
         }

         string arguments = configuration.Arguments.Replace( "%1", parameter );

         return _startProcess( configuration.FilePath, arguments );
      }
   }
}
