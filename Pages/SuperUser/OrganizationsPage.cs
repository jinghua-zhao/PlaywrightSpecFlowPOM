﻿using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages.SuperUser
{
    public class OrganizationsPage
    {
        // Constructors
        private readonly IPage _user;


        public OrganizationsPage(Hooks.Hooks hooks)
        {
            _user = hooks.User;
        }

        // Selectors
        private ILocator SearchBy => _user.GetByLabel("Search");
        private ILocator SearchInput => _user.Locator("#SearchValue");
        private ILocator SearchButton => _user.GetByRole(AriaRole.Button, new() { Name = "Search" });
        private ILocator SortBy => _user.GetByLabel("Sort By");
        private ILocator SearchResult => _user.GetByRole(AriaRole.Cell, new() { Name = "Motor Vehicles, Department of" });
        private ILocator FirstSearchResult => _user.Locator("table#users tr:first-child td:first-child");

        //Actions and Assertions
        public async Task AssertPageContent()
        {
            //Assert that the correct URL has been reached
            await Assertions.Expect(_user).ToHaveURLAsync("https://localhost:44341/SuperUser/Organizations");

            //Assert that the search form elements are visible
            await Assertions.Expect(SearchBy).ToBeVisibleAsync();
            await Assertions.Expect(SearchInput).ToBeVisibleAsync();
            await Assertions.Expect(SearchButton).ToBeVisibleAsync();
            await Assertions.Expect(SortBy).ToBeVisibleAsync();
        }

        public async Task SearchAndEnter(string searchTerm)
        {
            //Select name from dropdown
            await SearchBy.SelectOptionAsync(new[] { "Name" });

            //Type the search term into the search input
            await SearchInput.FillAsync(searchTerm);

            //Assert that the search input has the text entered
            var searchInputInnerText = await SearchInput.InputValueAsync();
            searchInputInnerText.Should().Be(searchTerm);

            //Click the search button to submit the search
            await SearchButton.ClickAsync();
        }

        public async Task AssertSearchResult(string searchTerm)
        {
            //Assert the first result text
            await Assertions.Expect(FirstSearchResult).ToContainTextAsync(searchTerm);
        }
    }
}