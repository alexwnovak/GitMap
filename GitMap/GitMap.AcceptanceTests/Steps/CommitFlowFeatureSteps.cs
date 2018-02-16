using Moq;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class CommitFlowFeatureSteps
   {
      private string _configuredEditor;
      private const string _commitFilePath = @"C:\Git\Repo\.git\COMMIT_EDITMSG";

      private readonly Mock<IConfigurationReader> _configurationReaderMock = new Mock<IConfigurationReader>();
      private readonly Mock<IProcessRunner> _processRunnerMock = new Mock<IProcessRunner>();

      [Given( @"my commit editor is configured to be (.*)" )]
      public void GivenMyCommitEditorIsConfiguredToBeNotepad_Exe( string editorPath )
      {
         _configuredEditor = editorPath;

         var notepadConfiguration = new ConfigurationPair( _configuredEditor, "%1" );

         _configurationReaderMock.Setup( cr => cr.Read( WorkflowNames.CommitWorkflow ) ).Returns( notepadConfiguration );
      }

      [When( @"the application launches" )]
      public void WhenTheApplicationLaunches()
      {
         var factory = new AppControllerFactory( _configurationReaderMock.Object, _processRunnerMock.Object );

         var appController = factory.Create();

         appController.Run( new[] { _commitFilePath } );
      }

      [Then( @"my configured editor is launched with the file" )]
      public void ThenMyConfiguredCommitEditorIsLaunchedWithTheCommitFile()
      {
         _processRunnerMock.Verify( pr => pr.Run( _configuredEditor, _commitFilePath ), Times.Once() );
      }
   }
}
