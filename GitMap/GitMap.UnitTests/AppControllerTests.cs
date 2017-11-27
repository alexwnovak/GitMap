using Xunit;
using Moq;
using GitModel;

namespace GitMap.UnitTests
{
   public class AppControllerTests
   {
      [Fact]
      public void Run_PassesNoArguments_LaunchesUI()
      {
         // Arrange

         var appLauncherMock = new Mock<IAppLauncher>();

         // Act

         var appController = new AppController( appLauncherMock.Object );
         appController.Run( new string[0] );

         // Assert

         appLauncherMock.Verify( al => al.LaunchUI(), Times.Once() );
      }

      [Fact]
      public void Run_PassesCommitFile_LaunchesConfiguredCommitEditor()
      {
         // Arrange

         var appLauncherMock = new Mock<IAppLauncher>();

         // Act

         var appController = new AppController( appLauncherMock.Object );

         appController.Run( new [] { GitFileNames.CommitFileName } );

         // Assert

         appLauncherMock.Verify( al => al.LaunchCommitEditor( GitFileNames.CommitFileName ), Times.Once() );
      }
   }
}
