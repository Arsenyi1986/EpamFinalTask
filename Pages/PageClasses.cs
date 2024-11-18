using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    private readonly ILogger _logger;
    
    public LoginPage(IWebDriver driver, ILogger logger)
    {
        this.driver = driver;
        this._logger = logger;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private readonly By? _loginfield = By.CssSelector("#user-name");
    private readonly By? _passwordfield = By.CssSelector("#password");
    private readonly By? _submitbutton = By.CssSelector("#login-button");

    public By? LoginField 
    {
        get => _loginfield;
    }

    public By? PasswordField 
    {
        get => _passwordfield;
    }

    public void TextInput(string login, string password)
    {
        _logger.Information("Passing inputs into fields...");
        driver.FindElement(_loginfield).SendKeys(login);
        driver.FindElement(_passwordfield).SendKeys(password);
        _logger.Information("Done");
    }

    public void ClearField(By? field)
    {
        _logger.Information("Clearing {Field} field...", field);
        IWebElement? element = driver.FindElement(field);
        element.Clear();
        new Actions(driver).MoveToElement(element).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).Build().Perform();
        _logger.Information("Done");
    }

    public void Submit()
    {
        _logger.Information("Submitting form...");

        try
        {
            driver.FindElement(_submitbutton).Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("The 'Submit button' element was not found.");
        }
        catch (ElementNotInteractableException)
        {
            Console.WriteLine("The 'Submit button' element is not interactable");
        }

        _logger.Information("Done");
    }

    public string? ReturnErrorInfo()
    {
        try
        {
            IWebElement error_box = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".error-message-container")));

            return error_box.FindElement(By.TagName("h3")).Text;
        }
        catch (TimeoutException)
        {
            Console.WriteLine("Error message container was not found in expected amount of time.");
            return null;
        }
    }

    public string? ReturnDash()
    {
        try
        {
            IWebElement dash_logo = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".app_logo")));

            return dash_logo.Text;
        }
        catch (TimeoutException)
        {
            Console.WriteLine("The dashboard message was not found in expected amount of time.");
            return null;
        }
    }
}
