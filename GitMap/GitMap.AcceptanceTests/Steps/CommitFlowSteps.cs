using GitMap.AcceptanceTests.PageObjects;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class CommitFlowSteps
   {
      private readonly ScenarioContext _scenarioContext;

      public CommitFlowSteps( ScenarioContext scenarioContext )
      {
         _scenarioContext = scenarioContext;
      }

      [Given( "my commit editor has been configured to be (.*)" )]
      public void GivenMyCommitEditorIsConfigured( string editorPath )
      {
         var appControllerPageObject = new AppControllerPageObject();
         appControllerPageObject.AddCommitWorkflow( editorPath );

         _scenarioContext.Set( appControllerPageObject );
      }

      [When( "the application launches" )]
      public void WhenTheApplicationLaunches()
      {
      }
   }
}
