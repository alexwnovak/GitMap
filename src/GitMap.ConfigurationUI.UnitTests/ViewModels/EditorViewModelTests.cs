using Xunit;
using Moq;
using FluentAssertions;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.ConfigurationUI.Services;

namespace GitMap.ConfigurationUI.UnitTests.ViewModels
{
   public class EditorViewModelTests
   {
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

      [Fact]
      public void IsDirty_EditorPathChanges_IsDirtyIsTrue()
      {
         var viewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(), null, null );

         viewModel.IsDirty.Should().BeFalse();

         viewModel.EditorPath = "Something else";

         viewModel.IsDirty.Should().BeTrue();
      }

      [Fact]
      public void IsDirty_ArgumentsChanged_IsDirtyIsTrue()
      {
         var viewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(), null, null );

         viewModel.IsDirty.Should().BeFalse();

         viewModel.Arguments = "Something else";

         viewModel.IsDirty.Should().BeTrue();
      }

      [Fact]
      public void IsDirty_IsEnabledChanges_IsDirtyIsTrue()
      {
         var viewModel = new EditorViewModel( Mock.Of<IFileBrowserService>(), null, null );

         viewModel.IsDirty.Should().BeFalse();

         viewModel.IsEnabled = true;

         viewModel.IsDirty.Should().BeTrue();
      }

   }
}
