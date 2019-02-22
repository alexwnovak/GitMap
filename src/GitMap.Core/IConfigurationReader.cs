namespace GitMap.Core
{
   public interface IConfigurationReader
   {
      EditorConfiguration Read( string workflowName );
   }
}
