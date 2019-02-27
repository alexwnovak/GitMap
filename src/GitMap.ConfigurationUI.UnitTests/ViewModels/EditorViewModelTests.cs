using Xunit;
using FluentAssertions;
using GitMap.ConfigurationUI.ViewModels;

namespace GitMap.ConfigurationUI.UnitTests.ViewModels
{
   public class EditorViewModelTests
   {
      [Fact]
      public void BrowseCommand_ChoosesAFile_EditorPathBecomesTheChosenFile()
      {
         var viewModel = new EditorViewModel(
            () => "notepad.exe",
            "Something",
            "Something" );

         viewModel.BrowseCommand.Execute( null );

         viewModel.EditorPath.Should().Be( "notepad.exe" );
      }

      [Fact]
      public void BrowseCommand_CancelsOutOfChoosingAFile_EditorPathIsUnchanged()
      {
         var viewModel = new EditorViewModel(
            () => null,
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
         var viewModel = new EditorViewModel( () => null, null, null );

         viewModel.IsDirty.Should().BeFalse();

         viewModel.EditorPath = "Something else";

         viewModel.IsDirty.Should().BeTrue();
      }

      [Fact]
      public void IsDirty_ArgumentsChanged_IsDirtyIsTrue()
      {
         var viewModel = new EditorViewModel( () => null, null, null );

         viewModel.IsDirty.Should().BeFalse();

         viewModel.Arguments = "Something else";

         viewModel.IsDirty.Should().BeTrue();
      }

      [Fact]
      public void IsDirty_IsEnabledChanges_IsDirtyIsTrue()
      {
         var viewModel = new EditorViewModel( () => null, null, null );

         viewModel.IsDirty.Should().BeFalse();

         viewModel.IsEnabled = true;

         viewModel.IsDirty.Should().BeTrue();
      }
   }
}
