using Serilog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Pages;

public enum Browser
{
    Chrome,
    Firefox
}

public static class DriverFactory
{
    public static IWebDriver GetDriver(Browser browser, ILogger logger)
    {
        logger.Information("Starting setup for {Browser} driver...", browser);

        IWebDriver driver;
        switch (browser)
        {
            case Browser.Chrome:
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--disable-extensions");
                chromeOptions.AddArgument("--disable-gpu");
                driver = new ChromeDriver(chromeOptions);
                break;
            case Browser.Firefox:
                var firefoxOptions = new FirefoxOptions();
                firefoxOptions.AddArgument("-headless");
                driver = new FirefoxDriver(firefoxOptions);
                break;
            default:
                throw new ArgumentException($"Browser {browser} is not supported");
        }

        logger.Information("{Browser} driver setup complete", browser);
        return driver;
    }
}
