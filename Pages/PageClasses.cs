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
    }

    private By login_field = By.CssSelector("#user-name");
    private By password_field = By.CssSelector("#password");
    private By submit_button = By.CssSelector("#login-button");

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
        driver.FindElement(submit_button).Click();
    }

    public string ReturnErrorInfo()
    {
        IWebElement error_box = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".error-message-container")));

        return error_box.FindElement(By.TagName("h3")).Text;
    }

    public string ReturnDash()
    {
        IWebElement dash_logo = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".app_logo")));

        return dash_logo.Text;
    }
}
