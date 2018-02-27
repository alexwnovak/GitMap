﻿using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModel : ViewModelBase
   {
      private string _header;
      public string Header
      {
         get => _header;
         set => Set( nameof( Header ), ref _header, value );
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

      public EditorViewModel()
      {
         BrowseCommand = new RelayCommand( OnBrowseCommand );
      }

      private void OnBrowseCommand()
      {
      }
   }
}
