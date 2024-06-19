using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages.SuperUser
{
    public class AddOrganizationsPage
    {
        // Constructors
        private readonly IPage _user;


        public AddOrganizationsPage(Hooks.Hooks hooks)
        {
            _user = hooks.User;
        }

        // Selectors
        private ILocator BackButton => _user.GetByRole(AriaRole.Link, new() { Name = "Back" });
        private ILocator SaveButton => _user.GetByRole(AriaRole.Button, new() { Name = "Save" });
        private ILocator NameInput => _user.GetByLabel("Name");
        private ILocator OrganizationType => _user.GetByLabel("Organization Type *");

        // Actions and Assertions
        public async Task AssertPageContent()
        {
            //Assert that the correct URL has been reached
            await Assertions.Expect(_user).ToHaveURLAsync("https://localhost:44341/SuperUser/AddOrganization");

            await Assertions.Expect(NameInput).ToBeVisibleAsync();
            await Assertions.Expect(OrganizationType).ToBeVisibleAsync();
            await Assertions.Expect(BackButton).ToBeVisibleAsync();
            await Assertions.Expect(SaveButton).ToBeVisibleAsync();
        }

        public async Task AddOrganization(string orgname, string orgType)
        {
            await NameInput.FillAsync(orgname);
            await OrganizationType.SelectOptionAsync(new[] { orgType });
            await SaveButton.ClickAsync();
        }

        public async Task ClickBackButton()
        {
            await BackButton.ClickAsync();
        }

        public async Task ClickSaveButton()
        {
            await SaveButton.ClickAsync();
        }
    }
}
