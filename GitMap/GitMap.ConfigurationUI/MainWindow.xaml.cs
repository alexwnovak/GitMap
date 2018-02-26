using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GitMap.ConfigurationUI
{
   public partial class MainWindow : Window
   {
      public ICommand CloseCommand
      {
         get;
      }

      public MainWindow()
      {
         InitializeComponent();
         CloseCommand = new RelayCommand( Close );
      }
   }
}
