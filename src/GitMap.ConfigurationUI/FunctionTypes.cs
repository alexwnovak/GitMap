using GitMap.Core;

namespace GitMap.ConfigurationUI
{
   public delegate EditorConfiguration ReadConfigurationFunction( string workflowName );
   public delegate void WriteConfigurationFunction( string workflowName, EditorConfiguration editorConfiguration );
}
