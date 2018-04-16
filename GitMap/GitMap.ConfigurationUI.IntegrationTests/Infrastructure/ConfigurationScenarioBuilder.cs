using System;
using Moq;
using GalaSoft.MvvmLight.Ioc;

namespace GitMap.ConfigurationUI.IntegrationTests.Infrastructure
{
   internal class ConfigurationScenarioBuilder
   {
      private readonly ObjectComposer _objectComposer;

      private ConfigurationScenarioBuilder()
      {
         _objectComposer = new ObjectComposer( new SimpleIoc() );
         _objectComposer.Compose();

         With( Mock.Of<Services.IDialogService> );
      }

      public static ConfigurationScenarioBuilder Create() => new ConfigurationScenarioBuilder();

      public ConfigurationScenarioBuilder With<TClass>( Func<TClass> factory ) where TClass : class
      {
         _objectComposer.Container.Unregister<TClass>();
         _objectComposer.Container.Register( factory );
         return this;
      }

      public ConfigurationScenario Build() => new ConfigurationScenario( _objectComposer );
   }
}
