using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Pages;

public class DashboardPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly Serilog.Core.Logger _logger;
    private readonly By? _logo = By.CssSelector(".app_logo");

    public DashboardPage(IWebDriver driver, Serilog.Core.Logger logger)
    {
        this._driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        this._logger = logger;
    }

    public string GetDashboardText()
    {
        try
        {
            _logger.Information("Attempting to retrieve dashboard text...");
            return _wait.Until(ExpectedConditions.ElementIsVisible(_logo)).Text;
        }
        catch (TimeoutException)
        {
            _logger.Error("Dashboard logo was not found in the expected amount of time.");
            return string.Empty;
        }
    } 
}
