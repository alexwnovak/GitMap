using System;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class LaunchingGitMapForConfigurationSteps
   {
      private readonly ScenarioContext _scenarioContext;

      public LaunchingGitMapForConfigurationSteps( ScenarioContext scenarioContext )
      {
         _scenarioContext = scenarioContext;
      }

      [Given( "I have launched GitMap with no arguments" )]
      public void GivenILaunchGitMapWithNoArguments()
      {
         _scenarioContext.Pending();
      }

      [Then( @"the UI appears" )]
      public void ThenTheUIAppears()
      {
         _scenarioContext.Pending();
      }
   }
}
