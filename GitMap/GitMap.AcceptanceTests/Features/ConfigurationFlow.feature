Feature: Launching GitMap for configuration
	As a developer,
   I want to run GitMap to configure what applications serve as my editors
	So I can have a specialized editing experience for each Git scenario

@Acceptance
Scenario: Running GitMap from my desktop
	Given I have launched the application with no arguments
	Then the UI appears
