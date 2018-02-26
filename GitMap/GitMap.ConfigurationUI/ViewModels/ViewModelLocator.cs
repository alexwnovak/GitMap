using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class ViewModelLocator
   {
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider( () => SimpleIoc.Default );
         SimpleIoc.Default.Register<MainViewModel>();
      }

      public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();

      public static void Cleanup()
      {
      }
   }
}