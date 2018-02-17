using System;
using Xunit;
using FluentAssertions;

namespace GitMap.UnitTests
{
   public class ConfigurationWorkflowTests
   {
      [Fact]
      public void Constructor_ProcessRunnerIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new ConfigurationWorkflow( null );

         constructor.ShouldThrow<ArgumentNullException>();
      }
   }
}
