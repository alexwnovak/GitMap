using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.ViewModels;

namespace GitMap.ConfigurationUI
{
   public partial class App : Application
   {
      protected override void OnStartup( StartupEventArgs e )
      {
         SimpleIoc.Default.Register<IEditorViewModelFactory, EditorViewModelFactory>();
      }
   }
}
