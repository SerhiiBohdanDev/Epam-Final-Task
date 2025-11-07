using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinalTask.CoreLayer.WebDriver.WebDriverWrapper;
internal class WebDriverWrapper
{
    private readonly TimeSpan timeout;
    private readonly IWebDriver driver;

    private const int WaitTimeInSeconds = 10;

    public WebDriverWrapper(BrowserType browserType)
    {
        driver = WebDriverFactory.CreateWebDriver(browserType);
        timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);
    }

    public void NavigateTo(string url)
    {
        driver.Navigate().GoToUrl(url);
    }

    public void Maximize()
    {
        driver.Manage().Window.Maximize();
    }

    public string GetTitle() => driver.Title;

    public void Close()
    {
        driver.Quit();
        driver.Dispose();
    }

    public void EnterText(By by, string text)
    {
        var element = WaitForElementToBePresent(by, timeout);
        element.SendKeys(text);
    }

    public void ClearInputField(By by)
    {
        var element = WaitForElementToBePresent(by, timeout);
        // simply using the Clear() method doesn't work as it restores the previous values, this way it works correctly
        element.SendKeys(Keys.Control + "a");
        element.SendKeys(Keys.Delete);
    }

    public void ClickElement(By by)
    {
        var element = WaitForElementToBePresent(by, timeout);
        element.Click();
    }

    public IWebElement FindElement(By by) => WaitForElementToBePresent(by, timeout);

    private IWebElement WaitForElementToBePresent(By by, TimeSpan _timeout)
    {
        var wait = new WebDriverWait(driver, _timeout);
        return wait.Until(drv =>
        {
            try
            {
                var element = drv.FindElement(by);
                if (element != null && element.Displayed)
                    return element;
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("WaitForElementToBePresent method: 'NoSuchElementException' is found.");
            }

            return null;
        });
    }
}
