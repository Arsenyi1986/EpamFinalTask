using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    
    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private readonly By? login_field = By.CssSelector("#user-name");
    private readonly By? password_field = By.CssSelector("#password");
    private readonly By? submit_button = By.CssSelector("#login-button");

    public By? LoginField 
    {
        get => login_field;
    }

    public By? PasswordField 
    {
        get => password_field;
    }

    public void TextInput(string login, string password)
    {
        driver.FindElement(login_field).SendKeys(login);
        driver.FindElement(password_field).SendKeys(password);
    }

    public void ClearField(By? field)
    {
        IWebElement? element = driver.FindElement(field);
        element.Clear();
        new Actions(driver).MoveToElement(element).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).Build().Perform();
    }

    public void Submit()
    {
        try
        {
            driver.FindElement(submit_button).Click();
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
