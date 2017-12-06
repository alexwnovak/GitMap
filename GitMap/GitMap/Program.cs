using System.Collections.Generic;
using GitModel;

namespace GitMap
{
   internal static class Program
   {
      private static readonly IConfigurationReader _configurationReader = new ConfigurationReader();
      private static readonly IProcessRunner _processRunner = new ProcessRunner();

      private static IWorkflow CreateWorkflow( string workflowName ) =>
         new Workflow( workflowName, _configurationReader, _processRunner );

      private static int Main( string[] arguments )
      {
         var workflows = new Dictionary<string, IWorkflow>
         {
            [GitFileNames.CommitFileName] = CreateWorkflow( "CommitWorkflow" )
         };

         return new AppController( workflows ).Run( arguments );
      }
   }
}
