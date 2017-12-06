using FluentAssertions;
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
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( new ConfigurationPair( "file path", "arguments" ) );
         var processRunnerMock = new Mock<IProcessRunner>();

         var commitWorkflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, processRunnerMock.Object );

         commitWorkflow.Launch( null );

         processRunnerMock.Verify( pr => pr.Run( "file path", "arguments" ), Times.Once() );
      }

      [Fact]
      public void Launch_ConfigurationSpecifiesWhereTheFilePathGoes_EditorReceivesTheFilePath()
      {
         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( new ConfigurationPair( "file path", "-file %1" ) );
         var processRunnerMock = new Mock<IProcessRunner>();

         var commitWorkflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, processRunnerMock.Object );

         commitWorkflow.Launch( "COMMIT_EDITMSG" );

         processRunnerMock.Verify( pr => pr.Run( "file path", "-file COMMIT_EDITMSG" ), Times.Once() );
      }

      [Fact]
      public void Launch_EditorIsLaunched_ReturnsEditorProcessExitCode()
      {
         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( new ConfigurationPair( "does not matter", "does not matter" ) );
         var processRunnerMock = new Mock<IProcessRunner>();
         processRunnerMock.Setup( pr => pr.Run( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( 1 );

         var commitWorkflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, processRunnerMock.Object );

         int exitCode = commitWorkflow.Launch( "COMMIT_EDITMSG" );

         exitCode.Should().Be( 1 );
      }

      [Fact]
      public void Launch_NoEditorConfigured_ReturnsExitCode1()
      {
         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( ConfigurationPair.Empty );

         var commitWorkflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, Mock.Of<IProcessRunner>() );

         int exitCode = commitWorkflow.Launch( "COMMIT_EDITMSG" );

         exitCode.Should().Be( 1 );
      }
   }
}
