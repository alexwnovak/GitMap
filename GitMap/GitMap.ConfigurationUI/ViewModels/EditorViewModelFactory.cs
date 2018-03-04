using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.Services;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModelFactory : IEditorViewModelFactory
   {
      public EditorViewModel Create( string workflowName, string header )
      {
         return new EditorViewModel( SimpleIoc.Default.GetInstance<IFileBrowserService>(),
            workflowName,
            header );
      }
   }
}
