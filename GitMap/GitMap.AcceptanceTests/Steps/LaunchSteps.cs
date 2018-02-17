using TechTalk.SpecFlow;
using GitMap.AcceptanceTests.PageObjects;

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

      [Then( "(.*) is launched to edit the file" )]
      public void ThenMyConfiguredEditorIsLaunchedWithTheCommitFile( string configuredEditor )
      {
         var appControllerPageObject = _scenarioContext.Get<AppControllerPageObject>();
         appControllerPageObject.VerifyEditorLaunch( configuredEditor );
      }

      [Given( "I have launched the application with no arguments" )]
      public void GivenILaunchTheApplicationWithNoArguments()
      {
         var appControllerPageObject = new AppControllerPageObject();
         appControllerPageObject.AddConfigurationWorkflow();
         appControllerPageObject.Run( new string[0] );

         _scenarioContext.Set( appControllerPageObject );
      }

      [Then( "the UI appears" )]
      public void ThenTheUIAppears()
      {
         var appControllerPageObject = _scenarioContext.Get<AppControllerPageObject>();
         appControllerPageObject.VerifyConfigurationUILaunch();
      }
   }
}
