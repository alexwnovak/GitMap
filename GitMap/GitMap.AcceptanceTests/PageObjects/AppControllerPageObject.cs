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

      public void AddCommitWorkflow( string editorPath )
      {
         var commitConfiguration = new ConfigurationPair( editorPath, "%1" );
         _configurationReaderMock.Setup( cr => cr.Read( WorkflowNames.CommitWorkflow ) ).Returns( commitConfiguration );

         var commitWorkflow = new Workflow( WorkflowNames.CommitWorkflow, _configurationReaderMock.Object, _processRunnerMock.Object );
         _workflows[GitFileNames.CommitFileName] = commitWorkflow;
      }

      public void AddRebaseWorkflow( string editorPath )
      {
         var commitConfiguration = new ConfigurationPair( editorPath, "%1" );
         _configurationReaderMock.Setup( cr => cr.Read( WorkflowNames.RebaseWorkflow ) ).Returns( commitConfiguration );

         var rebaseWorkflow = new Workflow( WorkflowNames.RebaseWorkflow, _configurationReaderMock.Object, _processRunnerMock.Object );
         _workflows[GitFileNames.RebaseFileName] = rebaseWorkflow;
      }

      public void Run( string argument )
      {
         _filePath = argument;

         _appController = new AppController( _workflows, Mock.Of<IOutputController>() );
         _appController.Run( new[] { argument } );
      }

      public void VerifyCommitEditorLaunch( string configuredEditor )
      {
         _processRunnerMock.Verify( pr => pr.Run( configuredEditor, _filePath ), Times.Once() );
      }
   }
}
