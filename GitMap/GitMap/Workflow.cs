using GitMap.Core;

namespace GitMap
{
   public class Workflow : IWorkflow
   {
      private readonly string _workflowName;
      private readonly IConfigurationReader _configurationReader;
      private readonly IProcessRunner _processRunner;

      public Workflow( string workflowName, IConfigurationReader configurationReader, IProcessRunner processRunner )
      {
         _workflowName = workflowName;
         _configurationReader = configurationReader;
         _processRunner = processRunner;
      }

      public int Launch( string parameter )
      {
         var configuration = _configurationReader.Read( _workflowName );

         if ( configuration == ConfigurationPair.Empty )
         {
            return 1;
         }

         string arguments = configuration.Arguments.Replace( "%1", parameter );

         return _processRunner.Run( configuration.FilePath, arguments );
      }
   }
}
