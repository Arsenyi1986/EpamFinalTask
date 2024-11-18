using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Pages;

public static class DriverFactory
{
    public static IWebDriver GetDriver(string browser)
    {
        return browser.ToLower() switch
        {
            "firefox" => new FirefoxDriver(new FirefoxOptions()),
            "chrome" => new ChromeDriver(new ChromeOptions()),
            "edge" => new EdgeDriver(new EdgeOptions()),
            _ => throw new ArgumentException("Unsupported browser")
        };
    }
}
