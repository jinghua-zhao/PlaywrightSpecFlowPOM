using Microsoft.Playwright;
using PlaywrightSpecFlowPOM.Pages.SuperUser;

namespace PlaywrightSpecFlowPOM.Steps;

[Binding]
public class OrganizationsSearchSteps
{
    private readonly IPage _user;
    private readonly OrganizationsPage _organizationsPage;

    public OrganizationsSearchSteps(Hooks.Hooks hooks, OrganizationsPage organizationsPage)
    {
        _user = hooks.User;
        _organizationsPage = organizationsPage;
    }

    [Given(@"the super user is on the Organizations page")]
    public async Task GivenTheUserIsOnTheDuckDuckGoHomepage()
    {
        await _user.GotoAsync("https://localhost:44341/Security/LogOn?ReturnUrl=%2FSecurity%2FLogOn");
        await _user.GetByLabel("Email *").ClickAsync();
        await _user.GetByLabel("Email *").FillAsync("jinghua.zhao@state.ca.gov");
        await _user.GetByRole(AriaRole.Button, new() { Name = "Request key" }).ClickAsync();
        await _user.GetByRole(AriaRole.Button, new() { Name = "Log On" }).ClickAsync();

        //Go to the DuckDuckGo homepage
        await _user.GotoAsync("https://localhost:44341/SuperUser/Organizations");

        //Assert the page
        await _organizationsPage.AssertPageContent();
    }

    [When(@"the super user searches for '(.*)'")]
    public async Task WhenTheUserSearchesFor(string searchTerm)
    {
        //Type the search term and press enter
        await _organizationsPage.SearchAndEnter(searchTerm);
    }

    [Then(@"the search results show '(.*)' as the first result")]
    public async Task ThenTheSearchResultsShowAsTheFirstResult(string searchTerm)
    {
        //Assert the page content
        await _organizationsPage.AssertSearchResult(searchTerm);
    }
}