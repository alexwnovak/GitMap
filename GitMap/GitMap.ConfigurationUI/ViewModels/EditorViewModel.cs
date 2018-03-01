﻿using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GitMap.ConfigurationUI.Services;
using GitMap.Core;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModel : ViewModelBase
   {
      private readonly IConfigurationReader _configurationReader;
      private readonly IFileBrowserService _fileBrowserService;
      private readonly string _workflowName;

      public string Header
      {
         get;
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

      public ICommand LoadedCommand
      {
         get;
      }

      public ICommand BrowseCommand
      {
         get;
      }

      public EditorViewModel( IConfigurationReader configurationReader,
         IFileBrowserService fileBrowserService,
         string workflowName,
         string header )
      {
         _configurationReader = configurationReader ?? throw new ArgumentNullException( nameof( configurationReader ) );
         _fileBrowserService = fileBrowserService ?? throw new ArgumentNullException( nameof( fileBrowserService ) );
         _workflowName = workflowName;
         Header = header;

         LoadedCommand = new RelayCommand( OnLoadedCommand );
         BrowseCommand = new RelayCommand( OnBrowseCommand );
      }

      private void OnLoadedCommand()
      {
         var pair = _configurationReader.Read( _workflowName );

         EditorPath = pair.FilePath;
         Arguments = pair.Arguments;
      }

      private void OnBrowseCommand() => EditorPath = _fileBrowserService.PickSingleFile() ?? EditorPath;
   }
}
