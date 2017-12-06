using System.Collections.Generic;
using GitModel;

namespace GitMap
{
   internal static class Program
   {
      private static int Main( string[] arguments )
      {
         var commitWorkflow = new Workflow( "CommitWorkflow", new ConfigurationReader(), new ProcessRunner() );

         var workflows = new Dictionary<string, IWorkflow>
         {
            [GitFileNames.CommitFileName] = commitWorkflow
         };

         var appController = new AppController( workflows );
         return appController.Run( arguments );
      }
   }
}
