using System;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class CommitFlowFeatureSteps
   {
      [Given( @"I have a valid COMMIT_EDITMSG file" )]
      public void GivenIHaveAValidCOMMIT_EDITMSGFile()
      {
         ScenarioContext.Current.Pending();
      }

      [Given( @"my commit editor is configured to be notepad\.exe" )]
      public void GivenMyCommitEditorIsConfiguredToBeNotepad_Exe()
      {
         ScenarioContext.Current.Pending();
      }

      [When( @"the application launches" )]
      public void WhenTheApplicationLaunches()
      {
         ScenarioContext.Current.Pending();
      }

      [Then( @"my configured commit editor is launched with the commit file" )]
      public void ThenMyConfiguredCommitEditorIsLaunchedWithTheCommitFile()
      {
         ScenarioContext.Current.Pending();
      }
   }
}
