using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly Serilog.Core.Logger _logger;
    
    private readonly By? _loginField = By.CssSelector("#user-name");
    private readonly By? _passwordField = By.CssSelector("#password");
    private readonly By? _submitButton = By.CssSelector("#login-button");
    private readonly By? _errorBox = By.CssSelector(".error-message-container");

    public LoginPage(IWebDriver driver, Serilog.Core.Logger logger)
    {
        this._driver = driver;
        this._logger = logger;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public By? LoginField 
    {
        get => _loginField;
    }

    public By? PasswordField 
    {
        get => _passwordField;
    }

    public void EnterCredentials(string login, string password)
    {
        _logger.Information("Passing inputs into fields...");
        _driver.FindElement(_loginField).SendKeys(login);
        _driver.FindElement(_passwordField).SendKeys(password);
        _logger.Information("Done");
    }

    public void ClearField(By? field)
    {
        _logger.Information("Clearing {Field} field...", field);
        IWebElement? element = _driver.FindElement(field);
        element.Clear();
        new Actions(_driver).MoveToElement(element).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).Build().Perform();
        _logger.Information("Done");
    }

    public void CLickSubmitButton()
    {
        _logger.Information("Submitting form...");

        try
        {
            _driver.FindElement(_submitButton).Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("The 'Submit button' element was not found.");
        }
        catch (ElementNotInteractableException)
        {
            Console.WriteLine("The 'Submit button' element is not interactable");
        }
    }

    public string? ReturnErrorInfo()
    {
        try
        {
            IWebElement errorElement = _wait.Until(ExpectedConditions.ElementIsVisible(_errorBox));

            return errorElement.FindElement(By.CssSelector("h3")).Text;
        }
        catch (TimeoutException)
        {
            Console.WriteLine("Error message container was not found in expected amount of time.");
            return null;
        }
    }
}