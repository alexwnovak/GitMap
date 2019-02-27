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
         Container.Register<IFileBrowserService, FileBrowserService>();
         Container.Register<IDialogService, DialogService>();
         Container.Register( CreateEditorViewModels );
      }

      private IEnumerable<IEditorViewModel> CreateEditorViewModels()
      {
         var fileBrowserService = Container.GetInstance<IFileBrowserService>();

         return new[]
         {
            new EditorViewModel( fileBrowserService, WorkflowNames.CommitWorkflow, Resx.Commit ),
            new EditorViewModel( fileBrowserService, WorkflowNames.RebaseWorkflow, Resx.Rebase )
         };
      }
   }
}
