using System.Windows;
using GalaSoft.MvvmLight.Ioc;

namespace GitMap.ConfigurationUI
{
   public partial class App : Application
   {
      protected override void OnStartup( StartupEventArgs e )
      {
         new ObjectComposer( SimpleIoc.Default ).Compose();
      }
   }
}
