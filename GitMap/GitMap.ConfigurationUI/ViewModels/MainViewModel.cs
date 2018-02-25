using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      public ObservableCollection<EditorViewModel> EditorViewModels
      {
         get;
      }

      public MainViewModel( IEnumerable<EditorViewModel> editorViewModels )
      {
         EditorViewModels = new ObservableCollection<EditorViewModel>( editorViewModels );
      }
   }
}
