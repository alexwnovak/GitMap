using FluentAssertions;
using Moq;
using Xunit;
using GitMap.Core;

namespace GitMap.UnitTests
{
   public class WorkflowTests
   {
      [Fact]
      public void Launch_GetsCommitEditorConfiguration_LaunchesConfiguredEditor()
      {
         var configurationPair = new ConfigurationPair
         {
            FilePath = "file path",
            Arguments = "arguments"
         };

         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( configurationPair );
         var processRunnerMock = new Mock<IProcessRunner>();

         var workflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, processRunnerMock.Object );

         workflow.Launch( null );

         processRunnerMock.Verify( pr => pr.Run( "file path", "arguments" ), Times.Once() );
      }

      [Fact]
      public void Launch_ConfigurationSpecifiesWhereTheFilePathGoes_EditorReceivesTheFilePath()
      {
         var configurationPair = new ConfigurationPair
         {
            FilePath = "file path",
            Arguments = "-file %1"
         };

         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( configurationPair );
         var processRunnerMock = new Mock<IProcessRunner>();

         var workflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, processRunnerMock.Object );

         workflow.Launch( "COMMIT_EDITMSG" );

         processRunnerMock.Verify( pr => pr.Run( "file path", "-file COMMIT_EDITMSG" ), Times.Once() );
      }

      [Fact]
      public void Launch_EditorIsLaunched_ReturnsEditorProcessExitCode()
      {
         var configurationPair = new ConfigurationPair
         {
            FilePath = "does not matter",
            Arguments = "does not matter"
         };

         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( configurationPair );
         var processRunnerMock = new Mock<IProcessRunner>();
         processRunnerMock.Setup( pr => pr.Run( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( 1 );

         var workflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, processRunnerMock.Object );

         int exitCode = workflow.Launch( "COMMIT_EDITMSG" );

         exitCode.Should().Be( 1 );
      }

      [Fact]
      public void Launch_NoEditorConfigured_ReturnsExitCode1()
      {
         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( "CommitWorkflow" ) ).Returns( ConfigurationPair.Empty );

         var workflow = new Workflow( "CommitWorkflow", configurationReaderMock.Object, Mock.Of<IProcessRunner>() );

         int exitCode = workflow.Launch( "COMMIT_EDITMSG" );

         exitCode.Should().Be( 1 );
      }
   }
}
