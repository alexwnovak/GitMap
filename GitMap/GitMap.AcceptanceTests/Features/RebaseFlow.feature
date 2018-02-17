Feature: Rebase Flow
   As a developer
   When I launch an interactive rebase
   I want my configured commit editor to be launched

@Acceptance
Scenario: Editing an interactive rebase
   Given my rebase editor has been configured to be notepad.exe
   When the application launches with the argument C:\Repo\.git\git-rebase-todo
   Then notepad.exe is launched to edit the file
