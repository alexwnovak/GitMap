Feature: RebaseFlowFeature
   As a developer
   When I launch an interactive rebase
   I want my configured commit editor to be launched

@Acceptance
Scenario: Editing an interactive rebase
   Given my commit editor is configured to be notepad.exe
   When the application launches
   Then my configured editor is launched with the file
