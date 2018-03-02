using System;
using Xunit;
using Moq;
using FluentAssertions;
using GitMap.ConfigurationUI.Services;
using GitMap.ConfigurationUI.ViewModels;

namespace GitMap.ConfigurationUI.UnitTests.ViewModels
{
   public class MainViewModelTests
   {
      [Fact]
      public void Constructor_FactoryIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new MainViewModel( null, Mock.Of<IDialogService>() );

         constructor.Should().Throw<ArgumentNullException>();
      }

      [Fact]
      public void Constructor_DialogServiceIsNull_ThrowsArgumentNullException()
      {
         Action constructor = () => new MainViewModel( Mock.Of<IEditorViewModelFactory>(), null );

         constructor.Should().Throw<ArgumentNullException>();
      }
   }
}
