using Serilog;
using Pages;
using OpenQA.Selenium;
using FluentAssertions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]

namespace Tests;

public class UnitTests
{   
    private readonly Serilog.Core.Logger _logger;

    public UnitTests()
    {
        var logDirectory = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        var logFilePath = Path.Combine(logDirectory, "log.txt");

        _logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        _logger.Information("Starting tests...");
    }

    public IWebDriver DriverSetup(string browser)
    {
        var driver = DriverFactory.GetDriver(browser, _logger);
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        return driver;
    }

    [Theory]
    [InlineData("chrome", "rando", "rando", "Epic sadface: Username is required")]
    [InlineData("firefox", "rando", "rando", "Epic sadface: Username is required")]
    [InlineData("edge", "rando", "rando", "Epic sadface: Username is required")]
    public void EmptyFieldsReturnUsernameRequired(string browser, string username, string password, string result)
    {
        _logger.Information("Starting test with empty credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver, _logger);

        logPage.TextInput(username, password);
        logPage.ClearField(logPage.LoginField);
        logPage.ClearField(logPage.PasswordField);
        logPage.Submit();

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the login credentials were empty.");    
    }

    [Theory]
    [InlineData("chrome", "rando", "rando", "Password is required")]
    [InlineData("firefox", "rando", "rando", "Password is required")]
    [InlineData("edge", "rando", "rando", "Password is required")]
    public void EmptyPasswordReturnsPasswordRequired(string browser, string login, string password, string result)
    {
        _logger.Information("Starting test with empty password credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver, _logger);

        logPage.TextInput(login, password);
        logPage.ClearField(logPage.PasswordField);
        logPage.Submit();

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the password credentials were empty.");
    }

    [Theory]
    [InlineData("chrome", "standard_user", "secret_sauce", "Swag Labs")]
    [InlineData("firefox", "standard_user", "secret_sauce", "Swag Labs")]
    [InlineData("edge", "standard_user", "secret_sauce", "Swag Labs")]
    public void CorrectCredentialsReturnSwagLabs(string browser, string login, string password, string result)
    {
        _logger.Information("Starting test with valid credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver, _logger);

        logPage.TextInput(login, password);
        logPage.Submit();

        logPage.ReturnDash().Should().Be(result, because: "The expected message in web-element was not found with right credentials");
    }
}