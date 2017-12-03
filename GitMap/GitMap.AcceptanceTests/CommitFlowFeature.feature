Feature: CommitFlowFeature
   As a developer
   When I make a commit
   I want my configured commit editor to be launched

@Acceptance
Scenario: Making a commit
   Given I have a valid COMMIT_EDITMSG file
   And my commit editor is configured to be notepad.exe
   When the application launches
   Then my configured commit editor is launched with the commit file
