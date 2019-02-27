using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModel : ViewModelBase, IEditorViewModel
   {
      private readonly PickSingleFileFunction _pickSingleFile;

      public string WorkflowName
      {
         get;
      }

      public string Header
      {
         get;
      }

      private bool _isDirty;
      public bool IsDirty
      {
         get => _isDirty;
         set => Set( nameof( IsDirty ), ref _isDirty, value );
      }

      private bool _isEnabled;
      public bool IsEnabled
      {
         get => _isEnabled;
         set => Set( nameof( IsEnabled ), ref _isEnabled, value );
      }

      private string _editorPath;
      public string EditorPath
      {
         get => _editorPath;
         set => Set( nameof( EditorPath ), ref _editorPath, value );
      }

      private string _arguments;
      public string Arguments
      {
         get => _arguments;
         set => Set( nameof( Arguments ), ref _arguments, value );
      }

      public ICommand BrowseCommand
      {
         get;
      }

      public EditorViewModel( PickSingleFileFunction pickSingleFile,
         string workflowName,
         string header )
      {
         _pickSingleFile = pickSingleFile;
         WorkflowName = workflowName;
         Header = header;

         BrowseCommand = new RelayCommand( OnBrowseCommand );

         PropertyChanged += OnPropertyChanged;
      }

      private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
      {
         if ( e.PropertyName != nameof( IsDirty ) )
         {
            IsDirty = true;
         }
      }

      private void OnBrowseCommand() => EditorPath = _pickSingleFile() ?? EditorPath;
   }
}
