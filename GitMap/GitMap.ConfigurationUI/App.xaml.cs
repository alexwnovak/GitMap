using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.Services;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.Core;
using Resx = GitMap.ConfigurationUI.Properties.Resources;

namespace GitMap.ConfigurationUI
{
   public partial class App : Application
   {
      protected override void OnStartup( StartupEventArgs e )
      {
         SimpleIoc.Default.Register<IConfigurationReader, ConfigurationReader>();
         SimpleIoc.Default.Register<IConfigurationWriter, ConfigurationWriter>();
         SimpleIoc.Default.Register<IFileBrowserService, FileBrowserService>();
         SimpleIoc.Default.Register<IDialogService, DialogService>();
         SimpleIoc.Default.Register( CreateEditorViewModels );
      }

      private static IEnumerable<IEditorViewModel> CreateEditorViewModels()
      {
         var fileBrowserService = SimpleIoc.Default.GetInstance<IFileBrowserService>();

         return new[]
         {
            new EditorViewModel( fileBrowserService, WorkflowNames.CommitWorkflow, Resx.Commit ),
            new EditorViewModel( fileBrowserService, WorkflowNames.RebaseWorkflow, Resx.Rebase )
         };
      }
   }
}
