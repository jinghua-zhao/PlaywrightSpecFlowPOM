using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages
{
    public class DuckDuckGoHomePage
    {
        // Constructors
        private readonly IPage _user;

        
        public DuckDuckGoHomePage(Hooks.Hooks hooks)
        {
            _user = hooks.User;
        }

        // Selectors
        private ILocator SearchInput => _user.Locator("input[id='searchbox_input']");
        private ILocator SearchButton => _user.Locator("button[type='submit']");

        //Actions and Assertions
        public async Task AssertPageContent()
        {
            //Assert that the correct URL has been reached
            await Assertions.Expect(_user).ToHaveURLAsync("https://duckduckgo.com/");

            //Assert that the search input is visible
            await Assertions.Expect(SearchInput).ToBeVisibleAsync();

            //Assert that the search button is visible
            await Assertions.Expect(SearchButton).ToBeVisibleAsync();
        }

        public async Task SearchAndEnter(string searchTerm)
        {
            //Type the search term into the search input
            await SearchInput.FillAsync(searchTerm);

            //Assert that the search input has the text entered
            var searchInputInnerText = await SearchInput.InputValueAsync();
            searchInputInnerText.Should().Be(searchTerm);

            //Click the search button to submit the search
            await SearchButton.ClickAsync();
        }
    }
}
