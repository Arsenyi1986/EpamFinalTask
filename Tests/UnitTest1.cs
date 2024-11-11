// Task description
// Launch URL: https://www.saucedemo.com/

// UC-1 Test Login form with empty credentials:
// Type any credentials into "Username" and "Password" fields.
// Clear the inputs.
// Hit the "Login" button.
// Check the error messages: "Username is required".

// UC-2 Test Login form with credentials by passing Username:
// Type any credentials in username.
// Enter password.
// Clear the "Password" input.
// Hit the "Login" button.
// Check the error messages: "Password is required".

// UC-3 Test Login form with credentials by passing Username & Password:
// Type credentials in username which are under Accepted username are sections.
// Enter password as secret sauce.
// Click on Login and validate the title “Swag Labs” in the dashboard.

// Provide parallel execution, add logging for tests and use Data Provider to parametrize tests. Make sure that all tasks are supported by these 3 conditions: UC-1; UC-2; UC-3.

// Please, add task description as README.md into your solution!
// To perform the task use the various of additional options:
// Test Automation tool: Selenium WebDriver;
// Browsers: 1) Firefox; 2) Chrome;
// Locators: CSS ;
// Test Runner: xUnit;

// [Optional] Patterns: 1) Factory method; 2) Abstract Factory; 3) Chain of responsibility;
// [Optional] Test automation approach: BDD;
// Assertions: Fluent Assertion;
// [Optional] Loggers: SeriLog.

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Serilog;
using Pages;
using OpenQA.Selenium;
using FluentAssertions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]

namespace Tests;

public class UnitTests
{   
    private ILogger logger;

    public UnitTests()
    {
        var logDirectory = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        var logFilePath = Path.Combine(logDirectory, "log.txt");

        logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        logger.Information("Starting tests...");
    }

    public IWebDriver DriverSetup(string browser)
    {
        logger.Information("Setting up driver instance...");
        
        IWebDriver driver;
        switch (browser.ToLower())
        {
            case "firefox":
                logger.Information("Setting up FireFox driver...");
                var firefoxOptions = new FirefoxOptions();
                driver = new FirefoxDriver(firefoxOptions);
                logger.Information("Done");
                break;
            case "chrome":
            default:
                logger.Information("Setting up Chrome driver...");
                var chromeOptions = new ChromeOptions();
                driver = new ChromeDriver(chromeOptions);
                logger.Information("Done");
                break;
        }

        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        logger.Information("WebDriver setup done");

        return driver;
    }

    [Theory]
    [InlineData("chrome", "rando", "rando", "Epic sadface: Username is required")]
    [InlineData("firefox", "rando", "rando", "Epic sadface: Username is required")]
    public void EmptyFieldsReturnUsernameReq(string browser, string username, string password, string result)
    {
        logger.Information("Starting test with empty credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver);

        logger.Information("Passing inputs into fields...");
        logPage.TextInput(username, password);
        logger.Information("Done");
        logger.Information("Clearing Login field...");
        logPage.ClearLogin();
        logger.Information("Done");
        logger.Information("Clearing Password field...");
        logPage.ClearPassword();
        logger.Information("Done");
        logger.Information("Submiting form...");
        logPage.Submit();
        logger.Information("Done");

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the login credentials were empty.");    
    }

    [Theory]
    [InlineData("chrome", "rando", "rando", "Password is required")]
    [InlineData("firefox", "rando", "rando", "Password is required")]
    public void EmptyPasswordReturnsPasswordReq(string browser, string login, string password, string result)
    {
        logger.Information("Starting test with empty password credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver);

        logger.Information("Passing inputs into fields...");
        logPage.TextInput(login, password);
        logger.Information("Done");
        logger.Information("Clearing Password field...");
        logPage.ClearPassword();
        logger.Information("Done");
        logger.Information("Submiting form...");
        logPage.Submit();
        logger.Information("Done");

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the password credentials were empty.");
    }

    [Theory]
    [InlineData("chrome", "standard_user", "secret_sauce", "Swag Labs")]
    [InlineData("firefox", "standard_user", "secret_sauce", "Swag Labs")]
    public void CorrectCredReturnsSwagLabs(string browser, string login, string password, string result)
    {
        logger.Information("Starting test with valid credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver);

        logger.Information("Passing inputs into fields...");
        logPage.TextInput(login, password);
        logger.Information("Done");
        logger.Information("Submiting form...");
        logPage.Submit();
        logger.Information("Done");

        logPage.ReturnDash().Should().Be(result, because: "The expected message in web-element was not found with right credentials");
    }
}