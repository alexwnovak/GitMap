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
         Action constructor = () => new MainViewModel( null, Mock.Of<IConfigurationReader>(), Mock.Of<IDialogService>() );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void Constructor_ConfigurationReaderIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new MainViewModel( Mock.Of<IEditorViewModelFactory>(), null, Mock.Of<IDialogService>() );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void Constructor_DialogServiceIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new MainViewModel( Mock.Of<IEditorViewModelFactory>(), Mock.Of<IConfigurationReader>(), null );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void ExitingCommand_NoDirtyEditors_ExitingProceeds()
      {
         var editorViewModel = new EditorViewModel( Mock.Of<IConfigurationReader>(),
            Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" );

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object, Mock.Of<IConfigurationReader>(), Mock.Of<IDialogService>() );
         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_DiscardsChangesWhenPrompted_ExitingProceeds()
      {
         var dialogServiceMock = new Mock<IDialogService>();
         dialogServiceMock.Setup( ds => ds.ShowExitConfirmationDialog() ).Returns( ExitConfirmationResult.No );

         var editorViewModel = new EditorViewModel( Mock.Of<IConfigurationReader>(),
            Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            IsDirty = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object, Mock.Of<IConfigurationReader>(), dialogServiceMock.Object );
         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeFalse();
      }

      [Fact]
      public void ExitingCommand_AbortsExitingWhenPrompted_ExitingProceeds()
      {
         var dialogServiceMock = new Mock<IDialogService>();
         dialogServiceMock.Setup( ds => ds.ShowExitConfirmationDialog() ).Returns( ExitConfirmationResult.Cancel );

         var editorViewModel = new EditorViewModel( Mock.Of<IConfigurationReader>(),
            Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" )
         {
            IsDirty = true
         };

         var factoryMock = new Mock<IEditorViewModelFactory>();
         factoryMock.Setup( f => f.Create( It.IsAny<string>(), It.IsAny<string>() ) ).Returns( editorViewModel );

         var cancelEventArgs = new CancelEventArgs();

         var mainViewModel = new MainViewModel( factoryMock.Object, Mock.Of<IConfigurationReader>(), dialogServiceMock.Object );
         mainViewModel.ExitingCommand.Execute( cancelEventArgs );

         cancelEventArgs.Cancel.Should().BeTrue();
      }

   }
}
