using System.Collections.Generic;
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

      [Fact]
      public void Run_PassesCommitFileWithAPath_LaunchesCommitEditor()
      {
         string commitFilePath = $@"C:\SomeRepo\.git\{GitFileNames.CommitFileName}";

         // Arrange

         var appLauncherMock = new Mock<IAppLauncher>();

         // Act

         var appController = new AppController( appLauncherMock.Object );

         appController.Run( new[] { commitFilePath } );

         // Assert

         appLauncherMock.Verify( al => al.LaunchCommitEditor( commitFilePath ), Times.Once() );
      }

      [Fact]
      public void Run_ArgumentMatchesWorkflow_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Some File Path"] = workflowMock.Object
         };

         var appController = new AppController( workflows );

         appController.Run( new[] { "Some File Path" } );

         workflowMock.Verify( w => w.Launch( "Some File Path" ), Times.Once() );
      }

      [Fact]
      public void Run_ArgumentPartialPathMatchesWorkflow_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Commit.txt"] = workflowMock.Object
         };

         var appController = new AppController( workflows );

         appController.Run( new[] { @"C:\Repo\.git\Commit.txt" } );

         workflowMock.Verify( w => w.Launch( @"C:\Repo\.git\Commit.txt" ), Times.Once() );
      }

      [Fact]
      public void Run_MatchesTheArgumentAgainstMultiplePossibilities_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Commit.txt"] = Mock.Of<IWorkflow>(),
            ["Rebase.txt"] = workflowMock.Object
         };

         var appController = new AppController( workflows );

         appController.Run( new[] { @"C:\Repo\.git\Rebase.txt" } );

         workflowMock.Verify( w => w.Launch( @"C:\Repo\.git\Rebase.txt" ), Times.Once() );
      }

      [Fact]
      public void Run_ArgumentIsBlankAndMatchesWorkflow_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Commit.txt"] = Mock.Of<IWorkflow>(),
            [""] = workflowMock.Object
         };

         var appController = new AppController( workflows );

         appController.Run( new string[0] );

         workflowMock.Verify( w => w.Launch( string.Empty ), Times.Once() );
      }
   }
}
