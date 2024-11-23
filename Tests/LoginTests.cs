using Serilog;
using Pages;
using OpenQA.Selenium;
using FluentAssertions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]

namespace Tests;

public class LoginTests
{   
    private readonly Serilog.Core.Logger _logger;
    public LoginTests()
    {
        _logger = Logger.GetLogger();
    }

    public IWebDriver DriverSetup(Browser browser)
    {
        var driver = DriverFactory.GetDriver(browser, _logger);
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        return driver;
    }

    public static IEnumerable<object[]> GetBrowsersForUsernameRequired()
    {
        yield return new object[] { Browser.Chrome,"rando", "rando", "Username is required" };
        yield return new object[] { Browser.Firefox, "rando", "rando", "Username is required" };
    }
    public static IEnumerable<object[]> GetBrowsersForPasswordRequired()
    {
        yield return new object[] { Browser.Chrome, "rando", "rando", "Password is required" };
        yield return new object[] { Browser.Firefox, "rando", "rando", "Password is required" };
    }
    public static IEnumerable<object[]> GetBrowsersForCorrectCredentials()
    {
        yield return new object[] { Browser.Chrome, "standard_user", "secret_sauce", "Swag Labs" };
        yield return new object[] { Browser.Firefox, "standard_user", "secret_sauce", "Swag Labs" };
        yield return new object[] { Browser.Chrome, "performance_glitch_user", "secret_sauce", "Swag Labs" };
        yield return new object[] { Browser.Firefox, "performance_glitch_user", "secret_sauce", "Swag Labs" };
        yield return new object[] { Browser.Chrome, "visual_user", "secret_sauce", "Swag Labs" };
        yield return new object[] { Browser.Firefox, "visual_user", "secret_sauce", "Swag Labs" };
    }

    [Theory]
    [MemberData(nameof(GetBrowsersForUsernameRequired))]
    public void EmptyFieldsReturnsUsernameRequired(Browser browser, string username, string password, string result)
    {
        _logger.Information("Starting test with empty credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver, _logger);

        logPage.EnterCredentials(username, password);
        logPage.ClearField(logPage.LoginField);
        logPage.ClearField(logPage.PasswordField);
        logPage.CLickSubmitButton();

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the login credentials were empty.");    
    }

    [Theory]
    [MemberData(nameof(GetBrowsersForPasswordRequired))]
    public void EmptyPasswordReturnsPasswordRequired(Browser browser, string login, string password, string result)
    {
        _logger.Information("Starting test with empty password credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver, _logger);

        logPage.EnterCredentials(login, password);
        logPage.ClearField(logPage.PasswordField);
        logPage.CLickSubmitButton();

        logPage.ReturnErrorInfo().Should().Contain(result, because: "The expected error message was not displayed when the password credentials were empty.");
    }

    [Theory]
    [MemberData(nameof(GetBrowsersForCorrectCredentials))]
    public void CorrectCredentialsReturnSwagLabs(Browser browser, string login, string password, string result)
    {
        _logger.Information("Starting test with valid credentials...");

        using var driver = DriverSetup(browser);
        var logPage = new LoginPage(driver, _logger);

        logPage.EnterCredentials(login, password);
        logPage.CLickSubmitButton();

        var dashPage = new DashboardPage(driver, _logger);

        dashPage.Should().NotBeNull("Login was successful and should navigate to DashboardPage.");
        dashPage.GetDashboardText().Should().Be(result, because: "The expected dashboard text was not found after successful login.");
    }
}