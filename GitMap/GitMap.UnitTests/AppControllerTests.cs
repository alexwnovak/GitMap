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
      public void Run_WorkflowsAreNull_ThrowsArgumentException()
      {
         Action constructor = () => new AppController( null );

         constructor.ShouldThrow<ArgumentException>();
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

      [Fact]
      public void Run_ArgumentsAreNullAndMatchesWorkflow_LaunchesThatWorkflow()
      {
         var workflowMock = new Mock<IWorkflow>();

         var workflows = new Dictionary<string, IWorkflow>
         {
            ["Commit.txt"] = Mock.Of<IWorkflow>(),
            [""] = workflowMock.Object
         };

         var appController = new AppController( workflows );

         appController.Run( null );

         workflowMock.Verify( w => w.Launch( string.Empty ), Times.Once() );
      }

      [Fact]
      public void Run_NoMatchingWorkflowWasFound_ReturnsExitCode1()
      {
         var appController = new AppController( new Dictionary<string, IWorkflow>() );

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

         var appController = new AppController( workflows );

         int exitCode = appController.Run( new string[0] );

         exitCode.Should().Be( 0 );
      }
   }
}
