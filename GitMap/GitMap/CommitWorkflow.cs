namespace GitMap
{
   public class CommitWorkflow : IWorkflow
   {
      private readonly IConfigurationReader _configurationReader;
      private readonly IProcessRunner _processRunner;

      public CommitWorkflow( IConfigurationReader configurationReader, IProcessRunner processRunner )
      {
         _configurationReader = configurationReader;
         _processRunner = processRunner;
      }

      public void Launch( string parameter )
      {
         var configuration = _configurationReader.Read<CommitWorkflow>();

         _processRunner.Run( configuration.FilePath, configuration.Arguments );
      }
   }
}
