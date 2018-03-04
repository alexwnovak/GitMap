using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitMap.ConfigurationUI.Properties;
using GitMap.ConfigurationUI.Services;
using GitMap.Core;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class MainViewModel : ViewModelBase
   {
      private readonly IConfigurationReader _configurationReader;
      private readonly IConfigurationWriter _configurationWriter;
      private readonly IDialogService _dialogService;

      public ObservableCollection<EditorViewModel> EditorViewModels
      {
         get;
      } = new ObservableCollection<EditorViewModel>();

      public ICommand LoadedCommand
      {
         get;
      }

      public ICommand ExitingCommand
      {
         get;
      }

      public MainViewModel( IEditorViewModelFactory editorViewModelFactory,
         IConfigurationReader configurationReader,
         IConfigurationWriter configurationWriter,
         IDialogService dialogService )
      {
         if ( editorViewModelFactory == null )
         {
            throw new ArgumentNullException( nameof( editorViewModelFactory ) );
         }

         _configurationReader = configurationReader ?? throw new ArgumentNullException( nameof( configurationReader ) );
         _configurationWriter = configurationWriter ?? throw new ArgumentNullException( nameof( configurationWriter ) );
         _dialogService = dialogService ?? throw new ArgumentNullException( nameof( dialogService ) );

         AddEditor( editorViewModelFactory, WorkflowNames.CommitWorkflow, Resources.Commit );
         AddEditor( editorViewModelFactory, WorkflowNames.RebaseWorkflow, Resources.Rebase );

         LoadedCommand = new RelayCommand( OnLoadedCommand );
         ExitingCommand = new RelayCommand<CancelEventArgs>( OnExitingCommand );
      }

      private void AddEditor( IEditorViewModelFactory editorViewModelFactory, string workflowName, string header )
      {
         var editor = editorViewModelFactory.Create( workflowName, header );
         EditorViewModels.Add( editor );
      }

      private void OnLoadedCommand()
      {
         foreach ( var editorViewModel in EditorViewModels )
         {
            var editorConfiguration = _configurationReader.Read( editorViewModel.WorkflowName );

            editorViewModel.Arguments = editorConfiguration.Arguments;
            editorViewModel.EditorPath = editorConfiguration.FilePath;
            editorViewModel.IsEnabled = editorConfiguration.IsEnabled;
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

               _configurationWriter.Write( editorViewModel.WorkflowName, editorConfiguration );
            }
         }
      }
   }
}
