using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class ViewModelLocator
   {
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider( () => SimpleIoc.Default );

         ////if (ViewModelBase.IsInDesignModeStatic)
         ////{
         ////   // Create design time view services and models
         ////   SimpleIoc.Default.Register<IDataService, DesignDataService>();
         ////}
         ////else
         ////{
         ////   // Create run time view services and models
         ////   SimpleIoc.Default.Register<IDataService, DataService>();
         ////}

         SimpleIoc.Default.Register<MainViewModel>();
      }

      public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();

      public static void Cleanup()
      {
      }
   }
}