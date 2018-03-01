using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.Services;
using GitMap.Core;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModelFactory : IEditorViewModelFactory
   {
      public EditorViewModel Create( string header )
      {
         return new EditorViewModel( SimpleIoc.Default.GetInstance<IConfigurationReader>(),
            SimpleIoc.Default.GetInstance<IFileBrowserService>(),
            WorkflowNames.CommitWorkflow,
            header );
      }
   }
}
