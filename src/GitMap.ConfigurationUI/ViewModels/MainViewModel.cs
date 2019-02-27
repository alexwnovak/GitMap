using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitMap.ConfigurationUI.Services;
using GitMap.Core;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      private readonly ReadConfigurationFunction _readConfiguration;
      private readonly WriteConfigurationFunction _writeConfiguration;
      private readonly IConfigurationWriter _configurationWriter;
      private readonly IDialogService _dialogService;

      public ObservableCollection<IEditorViewModel> EditorViewModels { get; } = new ObservableCollection<IEditorViewModel>();

      public ICommand LoadedCommand { get; }
      public ICommand ExitingCommand { get; }

      public MainViewModel( IEnumerable<IEditorViewModel> editorViewModels,
         ReadConfigurationFunction readConfiguration,
         WriteConfigurationFunction writeConfiguration,
         IDialogService dialogService )
      {
         _readConfiguration = readConfiguration;
         _writeConfiguration = writeConfiguration;
         _dialogService = dialogService;

         EditorViewModels = new ObservableCollection<IEditorViewModel>( editorViewModels );

         LoadedCommand = new RelayCommand( OnLoadedCommand );
         ExitingCommand = new RelayCommand<CancelEventArgs>( OnExitingCommand );
      }

      private void OnLoadedCommand()
      {
         foreach ( var editorViewModel in EditorViewModels )
         {
            var editorConfiguration = _readConfiguration( editorViewModel.WorkflowName );

            editorViewModel.Arguments = editorConfiguration.Arguments;
            editorViewModel.EditorPath = editorConfiguration.FilePath;
            editorViewModel.IsEnabled = editorConfiguration.IsEnabled;
            editorViewModel.IsDirty = false;
         }
      }

      private void OnExitingCommand( CancelEventArgs e )
      {
         bool promptToSaveChanges = EditorViewModels.Any( evm => evm.IsDirty );

         if ( promptToSaveChanges )
         {
            var result = _dialogService.ShowExitConfirmationDialog();

            if ( result == ExitConfirmationResult.Cancel )
            {
               e.Cancel = true;
               return;
            }
            if ( result == ExitConfirmationResult.No )
            {
               return;
            }

            foreach ( var editorViewModel in EditorViewModels )
            {
               var editorConfiguration = new EditorConfiguration
               {
                  FilePath = editorViewModel.EditorPath,
                  Arguments = editorViewModel.Arguments,
                  IsEnabled = editorViewModel.IsEnabled
               };

               _writeConfiguration( editorViewModel.WorkflowName, editorConfiguration );
            }
         }
      }
   }
}
