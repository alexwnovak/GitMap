namespace GitMap.ConfigurationUI.ViewModels
{
   public interface IEditorViewModelFactory
   {
      EditorViewModel Create( string workflowName, string header );
   }
}
