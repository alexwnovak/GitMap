using System;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class LaunchingGitMapForConfigurationSteps
   {
      [Given( @"I launch GitMap with no arguments" )]
      public void GivenILaunchGitMapWithNoArguments()
      {
         ScenarioContext.Current.Pending();
      }

      [Then( @"the UI appears" )]
      public void ThenTheUIAppears()
      {
         ScenarioContext.Current.Pending();
      }
   }
}
