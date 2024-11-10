using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Pages;

public class LoginPage
{
    private IWebDriver driver;
    
    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private By login_field = By.Id("user-name");
    private By password_field = By.Id("password");
    private By submit_button = By.Id("login-button");

    public void TextInput(string login, string password)
    {
        
    }
}
