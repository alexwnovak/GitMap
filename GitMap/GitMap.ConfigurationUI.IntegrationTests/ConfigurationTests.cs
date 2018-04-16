using Xunit;
using GitMap.ConfigurationUI.IntegrationTests.Infrastructure;

namespace GitMap.ConfigurationUI.IntegrationTests
{
   public class ConfigurationTests
   {
      [Fact]
      public void CommitEditorDetailsAreEditedThenSaved()
      {
         var configurationScenario = ConfigurationScenarioBuilder.Create().Build();
      }
   }
}
