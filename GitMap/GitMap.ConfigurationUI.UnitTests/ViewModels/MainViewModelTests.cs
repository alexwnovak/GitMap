using System;
using System.ComponentModel;
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
      public void Constructor_FactoryIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new MainViewModel( null,
            Mock.Of<IConfigurationReader>(),
            Mock.Of<IConfigurationWriter>(),
            Mock.Of<IDialogService>() );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void ExitingCommand_NoDirtyEditors_ExitingProceeds()
      {
         var editorViewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" );

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object,
            Mock.Of<IConfigurationReader>(),
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

         var editorViewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            IsDirty = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object,
            Mock.Of<IConfigurationReader>(),
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

         var editorViewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            IsDirty = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object,
            Mock.Of<IConfigurationReader>(),
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

         var editorViewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            IsDirty = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object,
            Mock.Of<IConfigurationReader>(),
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

         var configurationWriterMock = new Mock<IConfigurationWriter>();

         var editorViewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            EditorPath = "editor",
            Arguments = "arguments",
            IsEnabled = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object,
            Mock.Of<IConfigurationReader>(),
            configurationWriterMock.Object,
            dialogServiceMock.Object );

         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         configurationWriterMock.Verify( cw => cw.Write( It.IsAny<string>(), It.Is<EditorConfiguration>( ec =>
            ec.FilePath == "editor" && ec.Arguments == "arguments" && ec.IsEnabled ) ) );
      }

      [Fact]
      public void LoadedCommand_EditorsHaveSettings_SettingsAreLoaded()
      {
         var editorViewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            IsDirty = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var editorConfiguration = new EditorConfiguration
         {
            Arguments = "arguments",
            FilePath = "filePath",
            IsEnabled = true
         };

         var configurationReaderMock = new Mock<IConfigurationReader>();
         configurationReaderMock.Setup( cr => cr.Read( It.IsAny<string>() ) ).Returns( editorConfiguration );

         var mainViewModel = new MainViewModel( factoryMock.Object,
            configurationReaderMock.Object,
            Mock.Of<IConfigurationWriter>(),
            Mock.Of<DialogService>() );

         mainViewModel.LoadedCommand.Execute( null );

         editorViewModel.Arguments.Should().Be( "arguments" );
         editorViewModel.EditorPath.Should().Be( "filePath" );
         editorViewModel.IsEnabled.Should().BeTrue();
      }
   }
}
