Feature: Commit Flow
   As a developer
   When I make a commit
   I want my configured commit editor to be launched

@Acceptance
Scenario: Making a commit
   Given my commit editor has been configured to be notepad.exe
   When the application launches with the argument C:\Repo\.git\COMMIT_EDITMSG
   Then notepad.exe is launched to edit the file
