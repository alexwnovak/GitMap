using Moq;
using TechTalk.SpecFlow;

namespace GitMap.AcceptanceTests.Steps
{
   [Binding]
   public class EditorSteps
   {
      private readonly ScenarioContext _scenarioContext;

      public EditorSteps( ScenarioContext scenarioContext )
      {
         _scenarioContext = scenarioContext;
      }

      [Then( @"my configured editor is launched with the file" )]
      public void ThenMyConfiguredEditorIsLaunchedWithTheFile()
      {
         var processRunnerMock = _scenarioContext.Get<Mock<IProcessRunner>>();
         string configuredEditor = (string) _scenarioContext["configuredEditor"];
         string filePath  = (string) _scenarioContext["filePath"];

         processRunnerMock.Verify( pr => pr.Run( configuredEditor, filePath ), Times.Once() );
      }
   }
}
