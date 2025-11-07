using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace FinalTask.CoreLayer.WebDriver;
internal static class WebDriverFactory
{
    public static IWebDriver CreateWebDriver(BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Edge => new EdgeDriver(),
            _ => throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null),
        };
    }
}
