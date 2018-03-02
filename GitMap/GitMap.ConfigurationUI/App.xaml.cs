using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.Services;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.Core;

namespace GitMap.ConfigurationUI
{
   public partial class App : Application
   {
      protected override void OnStartup( StartupEventArgs e )
      {
         SimpleIoc.Default.Register<IEditorViewModelFactory, EditorViewModelFactory>();
         SimpleIoc.Default.Register<IConfigurationReader, ConfigurationReader>();
         SimpleIoc.Default.Register<IFileBrowserService, FileBrowserService>();
         SimpleIoc.Default.Register<IDialogService, DialogService>();
      }
   }
}
