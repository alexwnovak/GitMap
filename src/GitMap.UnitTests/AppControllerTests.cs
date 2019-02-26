using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Moq;

namespace GitMap.UnitTests
{
   public class AppControllerTests
   {
      [Fact]
      public void Run_ArgumentMatchesWorkflow_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Some File Path"] = workflowMock.Object
         };

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

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

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

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

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

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

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

         appController.Run( new string[0] );

         workflowMock.Verify( w => w.Launch( string.Empty ), Times.Once() );
      }

      [Fact]
      public void Run_ArgumentsAreNullAndMatchesWorkflow_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Commit.txt"] = Mock.Of<IWorkflow>(),
            [""] = workflowMock.Object
         };

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

         appController.Run( null );

         workflowMock.Verify( w => w.Launch( string.Empty ), Times.Once() );
      }

      [Fact]
      public void Run_NoMatchingWorkflowWasFound_ReturnsExitCode1()
      {
         var appController = new AppController(
            () => new Dictionary<string, IWorkflow>(),
            () => { },
            _ => { } );

         int exitCode = appController.Run( new string[0] );

         exitCode.Should().Be( 1 );
      }

      [Fact]
      public void Run_WorkflowIsRun_ReturnsExitCode0()
      {
         var workflows = new Dictionary<string, IWorkflow>
         {
            [""] = Mock.Of<IWorkflow>()
         };

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

         int exitCode = appController.Run( new string[0] );

         exitCode.Should().Be( 0 );
      }

      [Fact]
      public void Run_WorkflowReturnsExitCode_ExitCodeIsReturnedToEnvironment()
      {
         var workflowMock = new Mock<IWorkflow>();
         workflowMock.Setup( w => w.Launch( It.IsAny<string>() ) ).Returns( 123 );

         var workflows = new Dictionary<string, IWorkflow>
         {
            [""] = workflowMock.Object
         };

         var appController = new AppController(
            () => workflows,
            () => { },
            _ => { } );

         int exitCode = appController.Run( new string[0] );

         exitCode.Should().Be( 123 );
      }

      [Fact]
      public void Run_AppIsLaunched_DisplaysBanner()
      {
         bool bannerDisplayed = false;

         var appController = new AppController(
            () => new Dictionary<string, IWorkflow>(),
            () => bannerDisplayed = true,
            _ => { } );

         appController.Run( new string[0] );

         bannerDisplayed.Should().BeTrue();
      }

      [Fact]
      public void Run_NoConfigurationForWorkflow_DisplaysError()
      {
         bool errorDisplayed = false;

         var appController = new AppController(
            () => new Dictionary<string, IWorkflow>(),
            () => { },
            f => errorDisplayed = f == "COMMIT_MSG" );

         appController.Run( new[] { @"C:\Repo\.git\COMMIT_EDITMSG" } );
      }
   }
}
