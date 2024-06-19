Feature: Super User Organizations Search Page
	Search and add organization as super user

Scenario: Search for Motor on Organizations
	Given the super user is on the Organizations page
	When the super user searches for 'Motor'
	Then the search results show 'Motor Vehicles, Department of' as the first result