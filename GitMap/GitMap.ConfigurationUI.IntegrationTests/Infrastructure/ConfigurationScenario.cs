using System;
using System.Collections.Generic;
using System.Linq;
using GitMap.ConfigurationUI.ViewModels;
using GitMap.Core;

namespace GitMap.ConfigurationUI.IntegrationTests.Infrastructure
{
   internal class ConfigurationScenario
   {
      private readonly ObjectComposer _objectComposer;

      public ConfigurationScenario( ObjectComposer objectComposer )
      {
         _objectComposer = objectComposer;
      }

      private IEditorViewModel GetWorkflow( string workflowName )
      {
         var viewModels = _objectComposer.Container.GetInstance<IEnumerable<IEditorViewModel>>();
         return viewModels.Single( vm => vm.WorkflowName == workflowName );
      }

      public void SetCommitEditorPath( string editorPath )
      {
         var commitViewModel = GetWorkflow( WorkflowNames.CommitWorkflow );
         commitViewModel.EditorPath = editorPath;
      }

      public void SetCommitEditorArguments( string editorArguments )
      {
         var commitViewModel = GetWorkflow( WorkflowNames.CommitWorkflow );
         commitViewModel.Arguments = editorArguments;
      }

      public void SetCommitEditorIsEnabled( bool isEnabled )
      {
         var commitViewModel = GetWorkflow( WorkflowNames.CommitWorkflow );
         commitViewModel.IsEnabled = isEnabled;
      }

      public void AcceptChanges()
      {
         var mainViewModel = _objectComposer.Container.GetInstance<MainViewModel>();
         mainViewModel.ExitingCommand.Execute( null );
      }
   }
}
