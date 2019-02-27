using GitMap.ConfigurationUI.Services;
using GitMap.Core;

namespace GitMap.ConfigurationUI
{
   public delegate EditorConfiguration ReadConfigurationFunction( string workflowName );
   public delegate void WriteConfigurationFunction( string workflowName, EditorConfiguration editorConfiguration );
   public delegate ExitConfirmationResult ShowExitConfirmationFunction();
   public delegate string PickSingleFileFunction();
}
