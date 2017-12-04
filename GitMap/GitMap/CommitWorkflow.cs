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

      public int Launch( string parameter )
      {
         var configuration = _configurationReader.Read<CommitWorkflow>();

         string arguments = configuration.Arguments.Replace( "%1", parameter );

         return _processRunner.Run( configuration.FilePath, arguments );
      }
   }
}
