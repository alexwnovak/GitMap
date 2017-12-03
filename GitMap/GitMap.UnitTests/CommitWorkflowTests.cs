using Moq;
using Xunit;

namespace GitMap.UnitTests
{
   public class CommitWorkflowTests
   {
      [Fact]
      public void Launch_GetsCommitEditorConfiguration_LaunchesConfiguredEditor()
      {
         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read<CommitWorkflow>() ).Returns( new ConfigurationPair( "file path", "arguments" ) );
         var processRunnerMock = new Mock<IProcessRunner>();

         var commitWorkflow = new CommitWorkflow( configurationReaderMock.Object, processRunnerMock.Object );

         commitWorkflow.Launch( null );

         processRunnerMock.Verify( pr => pr.Run( "file path", "arguments" ), Times.Once() );
      }

      [Fact]
      public void Launch_ConfigurationSpecifiesWhereTheFilePathGoes_EditorReceivesTheFilePath()
      {
         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read<CommitWorkflow>() ).Returns( new ConfigurationPair( "file path", "-file %1" ) );
         var processRunnerMock = new Mock<IProcessRunner>();

         var commitWorkflow = new CommitWorkflow( configurationReaderMock.Object, processRunnerMock.Object );

         commitWorkflow.Launch( "COMMIT_EDITMSG" );

         processRunnerMock.Verify( pr => pr.Run( "file path", "-file COMMIT_EDITMSG" ), Times.Once() );
      }
   }
}
