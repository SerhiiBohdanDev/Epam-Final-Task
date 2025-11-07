using FinalTask.CoreLayer.WebDriver;
using FinalTask.CoreLayer.WebDriver.WebDriverWrapper;

namespace FinalTask.TestLayer;

[TestFixture(BrowserType.Chrome)]
[TestFixture(BrowserType.Edge)]
internal abstract class BaseTest
{
    protected WebDriverWrapper driver;
    private readonly BrowserType browserType;

    protected BaseTest(BrowserType browserType)
    {
        this.browserType = browserType;
    }

    [SetUp]
    public void Setup()
    {
        driver = new WebDriverWrapper(browserType);
        driver.Maximize();
    }

    [TearDown]
    public void Teardown()
    {
        driver.Close();
    }
}
