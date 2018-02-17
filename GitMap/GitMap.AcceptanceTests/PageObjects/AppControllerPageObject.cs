using System.Collections.Generic;
using Moq;
using GitModel;

namespace GitMap.AcceptanceTests.PageObjects
{
   internal class AppControllerPageObject
   {
      private readonly Mock<IConfigurationReader> _configurationReaderMock = new Mock<IConfigurationReader>();
      private readonly Mock<IProcessRunner> _processRunnerMock = new Mock<IProcessRunner>();

      private readonly Dictionary<string, IWorkflow> _workflows = new Dictionary<string, IWorkflow>();
      private string _filePath;

      private AppController _appController;

      public void AddCommitWorkflow( string editorPath ) =>
         AddWorkflow( WorkflowNames.CommitWorkflow, GitFileNames.CommitFileName, editorPath );

      public void AddRebaseWorkflow( string editorPath ) =>
         AddWorkflow( WorkflowNames.RebaseWorkflow, GitFileNames.RebaseFileName, editorPath );

      private void AddWorkflow( string workflowName, string gitFileName, string editorPath )
      {
         var configurationPair = new ConfigurationPair( editorPath, "%1" );
         _configurationReaderMock.Setup( cr => cr.Read( workflowName ) ).Returns( configurationPair );
         _workflows[gitFileName] = new Workflow( workflowName, _configurationReaderMock.Object, _processRunnerMock.Object );
      }

      public void Run( string argument )
      {
         _filePath = argument;

         _appController = new AppController( _workflows, Mock.Of<IOutputController>() );
         _appController.Run( new[] { argument } );
      }

      public void VerifyEditorLaunch( string configuredEditor )
      {
         _processRunnerMock.Verify( pr => pr.Run( configuredEditor, _filePath ), Times.Once() );
      }
   }
}
