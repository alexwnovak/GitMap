using System;
using Xunit;
using FluentAssertions;
using GitMap.ConfigurationUI.ViewModels;

namespace GitMap.ConfigurationUI.UnitTests.ViewModels
{
   public class EditorViewModelTests
   {
      [Fact]
      public void Constructor_ConfigurationReaderIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new EditorViewModel( null, "Something", "Something" );

         constructor.Should().Throw<ArgumentNullException>();
      }
   }
}
