using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GitMap.ConfigurationUI.ViewModels;
using Resx = GitMap.ConfigurationUI.Properties.Resources;

namespace GitMap.ConfigurationUI
{
   public partial class App : Application
   {
      protected override void OnStartup( StartupEventArgs e )
      {
         var editorViewModels = new []
         {
            new EditorViewModel { Header = Resx.Commit, IsEnabled = true },
            new EditorViewModel { Header = Resx.Rebase, IsEnabled = false }
         };

         SimpleIoc.Default.Register<IEnumerable<EditorViewModel>>( () => editorViewModels );
      }
   }
}
