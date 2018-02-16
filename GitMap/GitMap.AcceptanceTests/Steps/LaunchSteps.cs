using GitMap.AcceptanceTests.PageObjects;
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

      [When( "the application launches with the argument (.*)" )]
      public void WhenTheApplicationLaunchesWithAnArgument( string argument )
      {
         var appControllerPageObject = _scenarioContext.Get<AppControllerPageObject>();
         appControllerPageObject.Run( argument );
      }

      [Then( "(.*) is launched with the commit file" )]
      public void ThenMyConfiguredEditorIsLaunchedWithTheCommitFile( string configuredEditor )
      {
         var appControllerPageObject = _scenarioContext.Get<AppControllerPageObject>();
         appControllerPageObject.VerifyCommitEditorLaunch( configuredEditor );
      }

      [Then( "my configured rebase editor is launched with the rebase file" )]
      public void ThenMyConfiguredRebaseEditorIsLaunchedWithTheRebaseFile()
      {
      }

      [Given( "I have launched the application with no arguments" )]
      public void GivenILaunchTheApplicationWithNoArguments()
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
