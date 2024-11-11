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

// [Optional] Patterns: 1) Factory method; 2) Abstract Factory; 3) Chaine of responsibility;
// [Optional] Test automation approach: BDD;
// Assertions: Fluent Assertion;
// [Optional] Loggers: SeriLog.

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using Serilog;
using Pages;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]

namespace Tests;

public class UnitTests : IDisposable
{
    private ChromeDriver driver;
    private LoginPage logPage;
    private ILogger logger;

    public UnitTests()
    {
        logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        logger.Information("Starting tests...");

        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        logPage = new LoginPage(driver);
    }

    [Theory]
    [InlineData("rando", "rando", "Epic sadface: Username is required")]
    public void EmptyFieldsReturnUsernameReq(string username, string password, string result)
    {
        logPage.TextInput(username, password);
        logPage.ClearLogin();
        logPage.ClearPassword();
        logPage.Submit();
        Assert.True(logPage.ReturnErrorInfo() == result);
    }

    [Theory]
    [InlineData("rando", "rando", "Epic sadface: Password is required")]
    public void EmptyPasswordReturnsPasswordReq(string login, string password, string result)
    {
        logPage.TextInput(login, password);
        logPage.ClearPassword();
        logPage.Submit();
        Assert.True(logPage.ReturnErrorInfo() == result);
    }

    [Theory]
    [InlineData("standard_user", "secret_sauce", "Swag Labs")]
    public void CorrectCredReturnsSwagLabs(string login, string password, string result)
    {
        logPage.TextInput(login, password);
        logPage.Submit();
        Assert.True(logPage.ReturnDash() == result);
    }

    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }
}