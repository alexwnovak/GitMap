using System.Collections.Generic;
using Moq;
using GitModel;
using GitMap.Core;

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

      public void AddConfigurationWorkflow() =>
         _workflows[""] = new ConfigurationWorkflow( _processRunnerMock.Object );

      private void AddWorkflow( string workflowName, string gitFileName, string editorPath )
      {
         var editorConfiguration = new EditorConfiguration
         {
            FilePath = editorPath,
            Arguments = "%1"
         };

         _configurationReaderMock.Setup( cr => cr.Read( workflowName ) ).Returns( editorConfiguration );
         _workflows[gitFileName] = new Workflow( workflowName, _configurationReaderMock.Object, _processRunnerMock.Object );
      }

      public void Run( params string[] arguments )
      {
         if ( arguments.Length > 0 )
         {
            _filePath = arguments[0];
         }

         _appController = new AppController( _workflows, Mock.Of<IOutputController>() );
         _appController.Run( arguments );
      }

      public void VerifyEditorLaunch( string configuredEditor )
      {
         _processRunnerMock.Verify( pr => pr.Run( configuredEditor, _filePath ), Times.Once() );
      }

      public void VerifyConfigurationUILaunch()
      {
         _processRunnerMock.Verify( pr => pr.Run(
            It.Is<string>( fn => fn.EndsWith( "GitMap.ConfigurationUI.exe" ) ), null ),
            Times.Once() );
      }
   }
}
