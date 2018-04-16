namespace GitMap.ConfigurationUI.IntegrationTests.Infrastructure
{
   internal class ConfigurationScenarioBuilder
   {
      private ConfigurationScenarioBuilder()
      {
      }

      public static ConfigurationScenarioBuilder Create() => new ConfigurationScenarioBuilder();

      public ConfigurationScenario Build() => new ConfigurationScenario();
   }
}
