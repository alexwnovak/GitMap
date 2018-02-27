using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GitMap.ConfigurationUI.Behaviors
{
   public class WindowDragBehavior : Behavior<FrameworkElement>
   {
      protected override void OnAttached()
      {
         AssociatedObject.MouseDown += OnMouseDown;
      }

      protected override void OnDetaching()
      {
         AssociatedObject.MouseDown -= OnMouseDown;
      }

      private void OnMouseDown( object sender, MouseButtonEventArgs e )
      {
         if ( e.LeftButton == MouseButtonState.Pressed )
         {
            Application.Current.MainWindow.DragMove();
         }
      }
   }
}
