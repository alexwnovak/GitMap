using System;
using System.Collections.Generic;
using GitModel;
using GitMap.Core;

namespace GitMap
{
   public class AppControllerFactory
   {
      private readonly Func<string, EditorConfiguration> _readConfiguration;
      private readonly Func<string, string, int> _startProcess;

      public AppControllerFactory()
         : this( ConfigurationReader.Read, ProcessRunner.Run )
      {
      }

      public AppControllerFactory(
         Func<string, EditorConfiguration> readConfiguration,
         Func<string, string, int> startProcess )
      {
         _readConfiguration = readConfiguration;
         _startProcess = startProcess;
      }

      private IWorkflow CreateWorkflow( string workflowName ) =>
         new Workflow( workflowName, _readConfiguration, _startProcess );

      public AppController Create()
      {
         var workflows = new Dictionary<string, IWorkflow>
         {
            [""] = new ConfigurationWorkflow( _startProcess ),
            [GitFileNames.CommitFileName] = CreateWorkflow( WorkflowNames.CommitWorkflow ),
            [GitFileNames.RebaseFileName] = CreateWorkflow( WorkflowNames.RebaseWorkflow )
         };

         return new AppController(
            workflows,
            OutputController.DisplayBanner,
            OutputController.DisplayConfigurationError );
      }
   }
}
