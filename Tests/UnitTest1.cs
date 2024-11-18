using Serilog;
using Pages;
using OpenQA.Selenium;
using FluentAssertions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]

namespace Tests;

public class UnitTests
{   
    private readonly ILogger logger;

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
        var driver = DriverFactory.GetDriver(browser);
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        logger.Information("WebDriver setup done");

        return driver;
    }

    [Theory]
    [InlineData("chrome", "rando", "rando", "Epic sadface: Username is required")]
    [InlineData("firefox", "rando", "rando", "Epic sadface: Username is required")]
    [InlineData("edge", "rando", "rando", "Epic sadface: Username is required")]
    public void EmptyFieldsReturnUsernameRequirements(string browser, string username, string password, string result)
    {
        logger.Information("Starting test with empty credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver);

        logger.Information("Passing inputs into fields...");
        logPage.TextInput(username, password);
        logger.Information("Done");
        logger.Information("Clearing Login field...");
        logPage.ClearField(logPage.LoginField);
        logger.Information("Done");
        logger.Information("Clearing Password field...");
        logPage.ClearField(logPage.PasswordField);
        logger.Information("Done");
        logger.Information("Submiting form...");
        logPage.Submit();
        logger.Information("Done");

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the login credentials were empty.");    
    }

    [Theory]
    [InlineData("chrome", "rando", "rando", "Password is required")]
    [InlineData("firefox", "rando", "rando", "Password is required")]
    [InlineData("edge", "rando", "rando", "Password is required")]
    public void EmptyPasswordReturnsPasswordRequirements(string browser, string login, string password, string result)
    {
        logger.Information("Starting test with empty password credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver);

        logger.Information("Passing inputs into fields...");
        logPage.TextInput(login, password);
        logger.Information("Done");
        logger.Information("Clearing Password field...");
        logPage.ClearField(logPage.PasswordField);
        logger.Information("Done");
        logger.Information("Submiting form...");
        logPage.Submit();
        logger.Information("Done");

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the password credentials were empty.");
    }

    [Theory]
    [InlineData("chrome", "standard_user", "secret_sauce", "Swag Labs")]
    [InlineData("firefox", "standard_user", "secret_sauce", "Swag Labs")]
    [InlineData("edge", "standard_user", "secret_sauce", "Swag Labs")]
    public void CorrectCredentialsReturnsSwagLabs(string browser, string login, string password, string result)
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