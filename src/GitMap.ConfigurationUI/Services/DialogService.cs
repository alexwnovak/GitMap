using System.Windows;

namespace GitMap.ConfigurationUI.Services
{
   public static class DialogService
   {
      public static ExitConfirmationResult ShowExitConfirmationDialog()
      {
         var result = MessageBox.Show( "Do you want to save your changes?", "GitMap", MessageBoxButton.YesNoCancel );

         switch ( result )
         {
            case MessageBoxResult.Yes: return ExitConfirmationResult.Yes;
            case MessageBoxResult.No: return ExitConfirmationResult.No;
            default: return ExitConfirmationResult.Cancel;
         }
      }
   }
}
