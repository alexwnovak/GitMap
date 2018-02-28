using GalaSoft.MvvmLight.Ioc;
using GitMap.Core;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModelFactory : IEditorViewModelFactory
   {
      public EditorViewModel Create( string header )
      {
         return new EditorViewModel( SimpleIoc.Default.GetInstance<IConfigurationReader>(),
            WorkflowNames.CommitWorkflow,
            header );
      }
   }
}
