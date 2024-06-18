using Microsoft.Playwright;


namespace PlaywrightSpecFlowPOM.Pages
{
    public class SearchResultsPage
    {
        // Constructors
        private readonly IPage _user;

        public SearchResultsPage(Hooks.Hooks hooks)
        {
            _user = hooks.User;
        }

        // Selectors
        private ILocator SearchInput => _user.Locator("input[id='search_form_input']");
        private ILocator ResultArticle(int nth) => _user.GetByTestId("result").Nth(nth);
        //We're using the single search result that we've located as 'ResultArticle' to locate the next 2 selectors
        private ILocator ResultHeading(int nth) => ResultArticle(nth).Locator("h2");
        private ILocator ResultLink(int nth) => ResultArticle(nth).Locator("a[data-testid='result-title-a']");

        // Actions and Assertions
        public async Task AssertPageContent(string searchTerm)
        {
            // Wait for page load
            await _user.WaitForLoadStateAsync();

            //Assert the page url
            await Assertions.Expect(_user).ToHaveURLAsync($"https://duckduckgo.com/?t=h_&q=Playwright&ia=web");

            //Assert the search input has the search term
            await Assertions.Expect(SearchInput).ToHaveValueAsync(searchTerm);
        }

        public async Task AssertSearchResultAtIndex(string searchTerm, int resultIndex, string expectedResultLink)
        {
            //Assert the first result text
            await Assertions.Expect(ResultHeading(resultIndex)).ToContainTextAsync(searchTerm);

            //Assert the first result link
            await Assertions.Expect(ResultLink(resultIndex)).ToHaveAttributeAsync("href", expectedResultLink);
        }
    }
}
