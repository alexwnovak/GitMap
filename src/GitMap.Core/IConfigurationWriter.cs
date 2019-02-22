namespace GitMap.Core
{
   public interface IConfigurationWriter
   {
      void Write( string workflowName, EditorConfiguration editorConfiguration );
   }
}
