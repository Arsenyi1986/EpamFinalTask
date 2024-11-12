using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Pages;

public class LoginPage
{
    private IWebDriver driver;
    private WebDriverWait wait;
    
    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        try
        {
            ValidateElements();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private By login_field;
    private By password_field;
    private By submit_button;

    public void ValidateElements()
    {
        try
        {
            login_field = By.CssSelector("#user-name");
            password_field = By.CssSelector("#password");
            submit_button = By.CssSelector("#login-button");
        }
        catch (NoSuchElementException)
        {
            throw new ArgumentException("Some necessary WebElements were not found.");
        }
    }

    public void TextInput(string login, string password)
    {
        driver.FindElement(login_field).SendKeys(login);
        driver.FindElement(password_field).SendKeys(password);
    }

    public void ClearLogin()
    {
        driver.FindElement(login_field).Clear();
        IWebElement loginInput = driver.FindElement(login_field);

        Actions actions = new Actions(driver);
        actions.MoveToElement(loginInput).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).Build().Perform();
    }

    public void ClearPassword()
    {
        driver.FindElement(password_field).Clear();
        IWebElement passwordInput = driver.FindElement(password_field);

        Actions actions = new Actions(driver);
        actions.MoveToElement(passwordInput).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).Build().Perform();
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

    public string ReturnErrorInfo()
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

    public string ReturnDash()
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
