using Moq;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class CommitFlowSteps
   {
      private string _configuredEditor;
      private const string _commitFilePath = @"C:\Git\Repo\.git\COMMIT_EDITMSG";

      private readonly Mock<IConfigurationReader> _configurationReaderMock = new Mock<IConfigurationReader>();
      private readonly Mock<IProcessRunner> _processRunnerMock = new Mock<IProcessRunner>();

      private readonly ScenarioContext _scenarioContext;

      public CommitFlowSteps( ScenarioContext scenarioContext )
      {
         _scenarioContext = scenarioContext;
      }

      [Given( @"my commit editor has been configured to be (.*)" )]
      public void GivenMyCommitEditorIsConfiguredToBeNotepad_Exe( string editorPath )
      {
         _configuredEditor = editorPath;

         var notepadConfiguration = new ConfigurationPair( _configuredEditor, "%1" );

         _configurationReaderMock.Setup( cr => cr.Read( WorkflowNames.CommitWorkflow ) ).Returns( notepadConfiguration );

         _scenarioContext.Set( _processRunnerMock );
         _scenarioContext["configuredEditor"] = editorPath;
         _scenarioContext["filePath"] = _commitFilePath;
      }

      [When( @"the application launches" )]
      public void WhenTheApplicationLaunches()
      {
         var factory = new AppControllerFactory( _configurationReaderMock.Object, _processRunnerMock.Object );

         var appController = factory.Create();

         appController.Run( new[] { _commitFilePath } );
      }
   }
}
