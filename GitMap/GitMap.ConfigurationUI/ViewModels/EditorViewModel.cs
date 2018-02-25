using GalaSoft.MvvmLight;

namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModel : ViewModelBase
   {
      private string _header;
      public string Header
      {
         get => _header;
         set => Set( nameof( Header ), ref _header, value );
      }

      private bool _isEnabled;
      public bool IsEnabled
      {
         get => _isEnabled;
         set => Set( nameof( IsEnabled ), ref _isEnabled, value );
      }
   }
}
