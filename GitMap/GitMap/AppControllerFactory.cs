using System.Collections.Generic;
using GitModel;

namespace GitMap
{
   public class AppControllerFactory
   {
      private readonly IConfigurationReader _configurationReader;
      private readonly IProcessRunner _processRunner;

      public AppControllerFactory()
         : this( new ConfigurationReader(), new ProcessRunner() )
      {
      }

      public AppControllerFactory( IConfigurationReader configurationReader, IProcessRunner processRunner )
      {
         _configurationReader = configurationReader;
         _processRunner = processRunner;
      }

      private IWorkflow CreateWorkflow( string workflowName ) =>
         new Workflow( workflowName, _configurationReader, _processRunner);

      public AppController Create()
      {
         var workflows = new Dictionary<string, IWorkflow>
         {
            [GitFileNames.CommitFileName] = CreateWorkflow( WorkflowNames.CommitWorkflow )
         };

         return new AppController( workflows );
      }
   }
}
