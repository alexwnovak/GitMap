using GalaSoft.MvvmLight.Ioc;

namespace GitMap.ConfigurationUI
{
   public class ObjectComposer
   {
      private readonly ISimpleIoc _container;

      public ObjectComposer( ISimpleIoc container )
      {
         _container = container;
      }
   }
}
