namespace GitMap.ConfigurationUI.ViewModels
{
   public interface IEditorViewModel
   {
      string WorkflowName { get; }
      string Header { get; }
      bool IsDirty { get; set; }
      bool IsEnabled { get; set; }
      string EditorPath { get; set; }
      string Arguments { get; set; }
   }
}
