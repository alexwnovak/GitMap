using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitMap.ConfigurationUI.Properties;
using GitMap.Core;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      public ObservableCollection<EditorViewModel> EditorViewModels
      {
         get;
      } = new ObservableCollection<EditorViewModel>();

      public ICommand LoadedCommand
      {
         get;
      }

      public MainViewModel( IEditorViewModelFactory editorViewModelFactory )
      {
         var commit = editorViewModelFactory.Create( WorkflowNames.CommitWorkflow, Resources.Commit );
         var rebase = editorViewModelFactory.Create( WorkflowNames.RebaseWorkflow, Resources.Rebase );

         EditorViewModels.Add( commit );
         EditorViewModels.Add( rebase );

         LoadedCommand = new RelayCommand( OnLoadedCommand );
      }

      private void OnLoadedCommand()
      {
         foreach ( var editorViewModel in EditorViewModels )
         {
            editorViewModel.LoadedCommand.Execute( null );
         }
      }
   }
}
