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
      public void Constructor_ConfigurationReaderIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new EditorViewModel( null,
            Mock.Of<IFileBrowserService>(),
            "Something",
            "Something" );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void Constructor_FileBrowserServiceIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new EditorViewModel( Mock.Of<IConfigurationReader>(),
            null,
            "Something",
            "Something" );

         constructor.Should().Throw<ArgumentNullException>();
      }
   }
}
