namespace GitMap.ConfigurationUI.ViewModels
{
   public class EditorViewModelFactory : IEditorViewModelFactory
   {
      public EditorViewModel Create( string header )
      {
         return new EditorViewModel( header );
      }
   }
}
