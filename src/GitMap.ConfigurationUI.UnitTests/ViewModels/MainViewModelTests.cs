﻿using System.ComponentModel;
using System.Linq;
using Xunit;
using Moq;
using FluentAssertions;
using GitMap.ConfigurationUI.Services;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.Core;

namespace GitMap.ConfigurationUI.UnitTests.ViewModels
{
   public class MainViewModelTests
   {
      [Fact]
      public void ExitingCommand_NoDirtyEditors_ExitingProceeds()
      {
         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( Enumerable.Empty<IEditorViewModel>(),
            w => EditorConfiguration.Empty,
            ( _, __ ) => { },
            () => default( ExitConfirmationResult ) );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_DiscardsChangesWhenPrompted_ExitingProceeds()
      {
         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( Enumerable.Empty<IEditorViewModel>(),
            w => EditorConfiguration.Empty,
            ( _, __ ) => { },
            () => ExitConfirmationResult.No );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_AbortsExitingWhenPrompted_ExitingProceeds()
      {
         var viewModelMock = new Mock<IEditorViewModel>();
         viewModelMock.SetupGet( vm => vm.IsDirty ).Returns( true );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( new[] { viewModelMock.Object },
            w => EditorConfiguration.Empty,
            ( _, __ ) => { },
            () => ExitConfirmationResult.Cancel );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeTrue();
      }

      [Fact]
      public void ExitingCommand_DoesNotSaveWhenPrompted_ChangesAreNotSaved()
      {
         var cancelEventArgs = new CancelEventArgs();

         bool configurationWritten = false;

         var mainViewModel = new MainViewModel( Enumerable.Empty<IEditorViewModel>(),
            w => EditorConfiguration.Empty,
            ( _, __ ) => configurationWritten = true,
            () => ExitConfirmationResult.No );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         configurationWritten.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_SavesChangesWhenPrompted_ChangesAreSaved()
      {
         var viewModelMock = new Mock<IEditorViewModel>();
         viewModelMock.SetupGet( vm => vm.IsDirty ).Returns( true );
         viewModelMock.SetupGet( vm => vm.EditorPath ).Returns( "editor" );
         viewModelMock.SetupGet( vm => vm.Arguments ).Returns( "arguments" );
         viewModelMock.SetupGet( vm => vm.IsEnabled ).Returns( true );

         var cancelEventArgs = new CancelEventArgs();

         EditorConfiguration actualConfiguration = null;

         var mainViewModel = new MainViewModel( new[] { viewModelMock.Object },
            w => EditorConfiguration.Empty,
            ( _, ec ) => actualConfiguration = ec,
            () => ExitConfirmationResult.Yes );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         actualConfiguration.FilePath.Should().Be( "editor" );
         actualConfiguration.Arguments.Should().Be( "arguments" );
         actualConfiguration.IsEnabled.Should().BeTrue();
      }

      [Fact]
      public void LoadedCommand_EditorsHaveSettings_SettingsAreLoaded()
      {
         var editorConfiguration = new EditorConfiguration
         {
            Arguments = "arguments",
            FilePath = "filePath",
            IsEnabled = true
         };

         var mainViewModel = new MainViewModel( Enumerable.Empty<IEditorViewModel>(),
            w => editorConfiguration,
            ( _, __ ) => { },
            () => default( ExitConfirmationResult ) );

         mainViewModel.LoadedCommand.Execute( null );
      }
   }
}
