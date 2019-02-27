using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.Services;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.Core;
using Resx = GitMap.ConfigurationUI.Properties.Resources;

namespace GitMap.ConfigurationUI
{
   public class ObjectComposer
   {
      public ISimpleIoc Container { get; }

      public ObjectComposer( ISimpleIoc container )
      {
         Container = container;
      }

      public void Compose()
      {
         Container.Register<ReadConfigurationFunction>( () => ConfigurationContext.Read );
         Container.Register<WriteConfigurationFunction>( () => ConfigurationContext.Write );
         Container.Register<PickSingleFileFunction>( () => FileBrowserService.PickSingleFile );
         Container.Register<ShowExitConfirmationFunction>( () => DialogService.ShowExitConfirmationDialog );
         Container.Register( CreateEditorViewModels );
      }

      private IEnumerable<IEditorViewModel> CreateEditorViewModels()
      {
         var pickSingleFile = Container.GetInstance<PickSingleFileFunction>();

         return new[]
         {
            new EditorViewModel( pickSingleFile, WorkflowNames.CommitWorkflow, Resx.Commit ),
            new EditorViewModel( pickSingleFile, WorkflowNames.RebaseWorkflow, Resx.Rebase )
         };
      }
   }
}
