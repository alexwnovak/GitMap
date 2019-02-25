using System;
using System.ComponentModel;
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
            Mock.Of<IConfigurationWriter>(),
            Mock.Of<IDialogService>() );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_DiscardsChangesWhenPrompted_ExitingProceeds()
      {
         var dialogServiceMock = new Mock<IDialogService>();
         dialogServiceMock.Setup( ds => ds.ShowExitConfirmationDialog() ).Returns( ExitConfirmationResult.No );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( Enumerable.Empty<IEditorViewModel>(),
            w => EditorConfiguration.Empty,
            Mock.Of<IConfigurationWriter>(),
            dialogServiceMock.Object );
         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_AbortsExitingWhenPrompted_ExitingProceeds()
      {
         var dialogServiceMock = new Mock<IDialogService>();
         dialogServiceMock.Setup( ds => ds.ShowExitConfirmationDialog() ).Returns( ExitConfirmationResult.Cancel );

         var viewModelMock = new Mock<IEditorViewModel>();
         viewModelMock.SetupGet( vm => vm.IsDirty ).Returns( true );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( new[] { viewModelMock.Object },
            w => EditorConfiguration.Empty,
            Mock.Of<IConfigurationWriter>(),
            dialogServiceMock.Object );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeTrue();
      }

      [Fact]
      public void ExitingCommand_DoesNotSaveWhenPrompted_ChangesAreNotSaved()
      {
         var dialogServiceMock = new Mock<IDialogService>();
         dialogServiceMock.Setup( ds => ds.ShowExitConfirmationDialog() ).Returns( ExitConfirmationResult.No );

         var configurationWriterMock = new Mock<IConfigurationWriter>();

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( Enumerable.Empty<IEditorViewModel>(),
            w => EditorConfiguration.Empty,
            configurationWriterMock.Object,
            dialogServiceMock.Object );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         configurationWriterMock.Verify( cw => cw.Write( It.IsAny<string>(), It.IsAny<EditorConfiguration>() ), Times.Never() );
      }

      [Fact]
      public void ExitingCommand_SavesChangesWhenPrompted_ChangesAreSaved()
      {
         var dialogServiceMock = new Mock<IDialogService>();
         dialogServiceMock.Setup( ds => ds.ShowExitConfirmationDialog() ).Returns( ExitConfirmationResult.Yes );

         var viewModelMock = new Mock<IEditorViewModel>();
         viewModelMock.SetupGet( vm => vm.IsDirty ).Returns( true );
         viewModelMock.SetupGet( vm => vm.EditorPath ).Returns( "editor" );
         viewModelMock.SetupGet( vm => vm.Arguments ).Returns( "arguments" );
         viewModelMock.SetupGet( vm => vm.IsEnabled ).Returns( true );

         var configurationWriterMock = new Mock<IConfigurationWriter>();

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( new[] { viewModelMock.Object },
            w => EditorConfiguration.Empty,
            configurationWriterMock.Object,
            dialogServiceMock.Object );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         configurationWriterMock.Verify( cw => cw.Write( It.IsAny<string>(), It.Is<EditorConfiguration>( ec =>
            ec.FilePath == "editor" && ec.Arguments == "arguments" && ec.IsEnabled ) ) );
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
            Mock.Of<IConfigurationWriter>(),
            Mock.Of<DialogService>() );

         mainViewModel.LoadedCommand.Execute( null );
      }
   }
}
