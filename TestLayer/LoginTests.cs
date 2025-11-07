using FinalTask.BusinessLayer;
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

    [TestCaseSource(nameof(ValidLoginData))]
    public void CorrectLoginTest(LoginModel loginModel)
    {
        var loginPage = new LoginPage(driver);
        loginPage
            .Open()
            .EnterUsername(loginModel.Username)
            .EnterPassword(loginModel.Password)
            .ClickLoginButton();

        Assert.That(driver.GetTitle, Is.EqualTo("Swag Labs"));
    }

    private static IEnumerable<LoginModel> ValidLoginData()
    {
        yield return new LoginModel() { Username = "standard_user", Password = "secret_sauce" };
        yield return new LoginModel() { Username = "locked_out_user", Password = "secret_sauce" };
        yield return new LoginModel() { Username = "problem_user", Password = "secret_sauce" };
        yield return new LoginModel() { Username = "performance_glitch_user", Password = "secret_sauce" };
        yield return new LoginModel() { Username = "error_user", Password = "secret_sauce" };
        yield return new LoginModel() { Username = "visual_user", Password = "secret_sauce" };
    }
}