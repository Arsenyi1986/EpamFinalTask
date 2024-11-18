using Serilog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Pages;

public static class DriverFactory
{
    public static IWebDriver GetDriver(string browser, ILogger logger)
    {
        logger.Information("Starting setup for {Browser} driver...", browser);

        IWebDriver driver;
        switch (browser.ToLower())
        {
            case "chrome":
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--disable-extensions");
                chromeOptions.AddArgument("--disable-gpu");
                driver = new ChromeDriver(chromeOptions);
                break;
            case "firefox":
                var firefoxOptions = new FirefoxOptions();
                firefoxOptions.AddArgument("-headless");
                driver = new FirefoxDriver(firefoxOptions);
                break;
            case "edge":
                var edgeOptions = new EdgeOptions();
                edgeOptions.AddArgument("headless");
                edgeOptions.AddArgument("disable-extensions");
                driver = new EdgeDriver(edgeOptions);
                break;
            default:
                throw new ArgumentException($"Browser {browser} is not supported");
        }

        logger.Information("{Browser} driver setup complete", browser);
        return driver;
    }
}
