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

    private By login_field = By.Id("user-name");
    private By password_field = By.Id("password");
    private By submit_button = By.Id("login-button");
    private IWebElement result_box;

    public void TextInput(string login, string password)
    {
        driver.FindElement(login_field).SendKeys(login);
        driver.FindElement(password_field).SendKeys(password);
        Thread.Sleep(1000);
    }

    public void ClearFields()
    {
        ClearLogin();
        ClearPassword();
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

    public string SubmitAndReturn()
    {
        CheckClearFields();
        driver.FindElement(submit_button).Click();

        result_box = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("error-message-container")));

        return result_box.FindElement(By.TagName("h3")).Text;
    }

    public void CheckClearFields()
    {
        if (driver.FindElement(login_field).GetAttribute("value") != null)
        {
            driver.FindElement(login_field).Clear();
        }
        else
        {
            Console.WriteLine("login's clear");
        }

        if (driver.FindElement(password_field).GetAttribute("value") != null)
        {
            driver.FindElement(password_field).Clear();
        }
        else
        {
            Console.WriteLine("password's clear");
        }
    }
}
