using Moq;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class LaunchSteps
   {
      private readonly ScenarioContext _scenarioContext;

      public LaunchSteps( ScenarioContext scenarioContext )
      {
         _scenarioContext = scenarioContext;
      }

      [Then( "my configured editor is launched with the file" )]
      public void ThenMyConfiguredEditorIsLaunchedWithTheFile()
      {
         var processRunnerMock = _scenarioContext.Get<Mock<IProcessRunner>>();
         string configuredEditor = (string) _scenarioContext["configuredEditor"];
         string filePath = (string) _scenarioContext["filePath"];

         processRunnerMock.Verify( pr => pr.Run( configuredEditor, filePath ), Times.Once() );
      }

      [Then( "my configured rebase editor is launched with the rebase file" )]
      public void ThenMyConfiguredRebaseEditorIsLaunchedWithTheRebaseFile()
      {
      }

      [Given( "I have launched the application with no arguments" )]
      public void GivenILaunchGitMapWithNoArguments()
      {
         var appController = new AppControllerFactory().Create();
         appController.Run( new string[0] );
      }

      [Then( "the UI appears" )]
      public void ThenTheUIAppears()
      {
         _scenarioContext.Pending();
      }
   }
}
