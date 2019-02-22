using System;
using System.Collections.Generic;
using GitModel;
using GitMap.Core;

namespace GitMap
{
   public class AppControllerFactory
   {
      private readonly IConfigurationReader _configurationReader;
      private readonly IProcessRunner _processRunner;
      private readonly Func<string, string, int> _startProcess;

      public AppControllerFactory()
         : this( new ConfigurationReader(), ProcessRunner.Run )
      {
      }

      public AppControllerFactory(
         IConfigurationReader configurationReader,
         Func<string, string, int> startProcess )
      {
         _configurationReader = configurationReader;
         _startProcess = startProcess;
      }

      private IWorkflow CreateWorkflow( string workflowName ) =>
         new Workflow( workflowName, _configurationReader, _processRunner );

      public AppController Create()
      {
         var workflows = new Dictionary<string, IWorkflow>
         {
            [""] = new ConfigurationWorkflow( _startProcess ),
            [GitFileNames.CommitFileName] = CreateWorkflow( WorkflowNames.CommitWorkflow )
         };

         return new AppController( workflows, new OutputController() );
      }
   }
}
