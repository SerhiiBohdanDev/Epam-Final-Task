using FinalTask.CoreLayer.WebDriver.WebDriverWrapper;
using OpenQA.Selenium;

namespace FinalTask.BusinessLayer.PageObjects;
internal class LoginPage
{

    private static string Url { get; } = "https://www.saucedemo.com/";
    private readonly WebDriverWrapper driver;

    public LoginPage(WebDriverWrapper driver)
    {
        this.driver = driver;
    }

    // using CSS selectors only because task requirements state so
    // username, password and button can be located by Id
    private readonly By UsernameLocator = By.CssSelector("#user-name");
    private readonly By PasswordLocator = By.CssSelector("#password");
    private readonly By LoginButtonLocator = By.CssSelector("#login-button");
    private readonly By ErrorMessageLocator = By.CssSelector("[data-test='error']");

    public LoginPage Open()
    {
        driver.NavigateTo(Url);
        return this;
    }

    public LoginPage EnterUsername(string username)
    {
        driver.EnterText(UsernameLocator, username);
        return this;
    }

    public LoginPage ClearUsername()
    {
        driver.ClearInputField(UsernameLocator);
        return this;
    }

    public LoginPage EnterPassword(string password)
    {
        driver.EnterText(PasswordLocator, password);
        return this;
    }
    public LoginPage ClearPassword()
    {
        driver.ClearInputField(PasswordLocator);
        return this;
    }

    public LoginPage ClickLoginButton()
    {
        driver.ClickElement(LoginButtonLocator);
        return this;
    }

    public string GetErrorMessage() => driver.FindElement(ErrorMessageLocator).Text;

}
