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
         var editorConfiguration = new EditorConfiguration
         {
            FilePath = "file path",
            Arguments = "arguments"
         };

         string path = null;
         string arguments = null;

         var workflow = new Workflow(
            "CommitWorkflow", 
            w => editorConfiguration,
            ( p, a ) => { path = p; arguments = a; return 0; } );

         workflow.Launch( null );

         path.Should().Be( "file path" );
         arguments.Should().Be( "arguments" );
      }

      [Fact]
      public void Launch_ConfigurationSpecifiesWhereTheFilePathGoes_EditorReceivesTheFilePath()
      {
         var editorConfiguration = new EditorConfiguration
         {
            FilePath = "file path",
            Arguments = "-file %1"
         };

         string path = null;
         string arguments = null;

         var workflow = new Workflow(
            "CommitWorkflow",
            w => editorConfiguration,
            ( p, a ) => { path = p; arguments = a; return 0; } );

         workflow.Launch( "COMMIT_EDITMSG" );

         path.Should().Be( "file path" );
         arguments.Should().Be( "-file COMMIT_EDITMSG" );
      }

      [Fact]
      public void Launch_EditorIsLaunched_ReturnsEditorProcessExitCode()
      {
         var editorConfiguration = new EditorConfiguration
         {
            FilePath = "does not matter",
            Arguments = "does not matter"
         };

         var workflow = new Workflow(
            "CommitWorkflow",
            w => editorConfiguration,
            ( _, __ ) => 1 );

         int exitCode = workflow.Launch( "COMMIT_EDITMSG" );

         exitCode.Should().Be( 1 );
      }

      [Fact]
      public void Launch_NoEditorConfigured_ReturnsExitCode1()
      {
         var workflow = new Workflow( "CommitWorkflow", w => EditorConfiguration.Empty, ( _, __ ) => 0 );

         int exitCode = workflow.Launch( "COMMIT_EDITMSG" );

         exitCode.Should().Be( 1 );
      }
   }
}
