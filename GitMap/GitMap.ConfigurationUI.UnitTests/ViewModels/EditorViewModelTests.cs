using System;
using Xunit;
using Moq;
using FluentAssertions;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.Core;
using GitMap.ConfigurationUI.Services;

namespace GitMap.ConfigurationUI.UnitTests.ViewModels
{
   public class EditorViewModelTests
   {
      [Fact]
      public void Constructor_FileBrowserServiceIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new EditorViewModel( null,
            "Something",
            "Something" );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void BrowseCommand_ChoosesAFile_EditorPathBecomesTheChosenFile()
      {
         var fileBrowserServiceMock = new Mock<IFileBrowserService>();
         fileBrowserServiceMock.Setup( fbs => fbs.PickSingleFile() ).Returns( "notepad.exe" );

         var viewModel = new EditorViewModel( fileBrowserServiceMock.Object,
            "Something",
            "Something" );

         viewModel.BrowseCommand.Execute( null );

         viewModel.EditorPath.Should().Be( "notepad.exe" );
      }

      [Fact]
      public void BrowseCommand_CancelsOutOfChoosingAFile_EditorPathIsUnchanged()
      {
         var fileBrowserServiceMock = new Mock<IFileBrowserService>();
         fileBrowserServiceMock.Setup( fbs => fbs.PickSingleFile() ).Returns<string>( null );

         var viewModel = new EditorViewModel( fileBrowserServiceMock.Object,
            "Something",
            "Something" )
         {
            EditorPath = "notepad.exe"
         };

         viewModel.BrowseCommand.Execute( null );

         viewModel.EditorPath.Should().Be( "notepad.exe" );
      }
   }
}
