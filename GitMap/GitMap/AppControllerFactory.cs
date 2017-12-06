using System.Collections.Generic;
using GitModel;

namespace GitMap
{
   public static class AppControllerFactory
   {
      private static readonly IConfigurationReader _configurationReader = new ConfigurationReader();
      private static readonly IProcessRunner _processRunner = new ProcessRunner();

      private static IWorkflow CreateWorkflow( string workflowName ) =>
         new Workflow( workflowName, _configurationReader, _processRunner );

      public static AppController Create()
      {
         var workflows = new Dictionary<string, IWorkflow>
         {
            [GitFileNames.CommitFileName] = CreateWorkflow( WorkflowNames.CommitWorkflow )
         };

         return new AppController( workflows );
      }
   }
}
