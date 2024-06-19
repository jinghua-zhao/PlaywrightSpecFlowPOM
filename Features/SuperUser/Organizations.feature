Feature: Organizations Page
	Search and add organization as super user

Scenario: Search with keyword of Motor
	Given the super user is on the Organizations page
	When the super user searches for 'Motor'
	Then the search results show 'Motor Vehicles, Department of' as the first result

Scenario: Navigate to and from the Add Organization page
    Given the super user is on the Organizations page
    When the super user clicks the Add Organization button
    Then the Add Organization page should be opened
    When the super user clicks the Back button
    Then the Organizations page should be opened

