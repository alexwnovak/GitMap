using System;
using System.Windows.Markup;
using GitMap.ConfigurationUI.Properties;

namespace GitMap.ConfigurationUI.Markup
{
   public class ResxExtension : MarkupExtension
   {
      private readonly string _resourceName;

      public ResxExtension( string resourceName )
      {
         _resourceName = resourceName ?? throw new ArgumentException( nameof( resourceName ) );
      }

      public override object ProvideValue( IServiceProvider serviceProvider ) =>
         Resources.ResourceManager.GetString( _resourceName );
   }
}
