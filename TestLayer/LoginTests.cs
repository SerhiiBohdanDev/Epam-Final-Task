using FinalTask.BusinessLayer.PageObjects;
using FinalTask.CoreLayer.WebDriver;

namespace FinalTask.TestLayer;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
internal class LoginTests : BaseTest
{
    public LoginTests(BrowserType browserType) : base(browserType)
    {
    }

    [TestCase("username", "password")]
    public void EmptyFieldsTest(string username, string password)
    {
        var loginPage = new LoginPage(driver);
        loginPage
            .Open()
            .EnterUsername(username)
            .EnterPassword(password)
            .ClearUsername()
            .ClearPassword()
            .ClickLoginButton();

        Assert.That(loginPage.GetErrorMessage(), Does.Contain("Username is required"));
    }

    [TestCase("username", "password")]
    public void EmptyPasswordFieldTest(string username, string password)
    {
        var loginPage = new LoginPage(driver);
        loginPage
            .Open()
            .EnterUsername(username)
            .EnterPassword(password)
            .ClearPassword()
            .ClickLoginButton();

        Assert.That(loginPage.GetErrorMessage(), Does.Contain("Password is required"));
    }

    [TestCase("standard_user", "secret_sauce")]
    [TestCase("locked_out_user", "secret_sauce")]
    [TestCase("problem_user", "secret_sauce")]
    [TestCase("performance_glitch_user", "secret_sauce")]
    [TestCase("error_user", "secret_sauce")]
    [TestCase("visual_user", "secret_sauce")]
    public void CorrectLoginTest(string username, string password)
    {
        var loginPage = new LoginPage(driver);
        loginPage
            .Open()
            .EnterUsername(username)
            .EnterPassword(password)
            .ClickLoginButton();

        Assert.That(driver.GetTitle, Is.EqualTo("Swag Labs"));
    }
}