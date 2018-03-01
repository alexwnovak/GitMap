using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GitMap.ConfigurationUI.Properties;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      public ObservableCollection<EditorViewModel> EditorViewModels
      {
         get;
      } = new ObservableCollection<EditorViewModel>();

      public MainViewModel( IEditorViewModelFactory editorViewModelFactory )
      {
         var commit = editorViewModelFactory.Create( Resources.Commit );
         var rebase = editorViewModelFactory.Create( Resources.Rebase );
         
         EditorViewModels.Add( commit );
         EditorViewModels.Add( rebase );
      }
   }
}
