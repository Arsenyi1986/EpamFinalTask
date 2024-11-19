# Final EPAM assignment

## Description
This project is a test automation suite built with Selenium WebDriver and xUnit to test the login functionality of a web application. It automates the testing of various user login scenarios to ensure correct behavior.

## Task
- Launch URL: https://www.saucedemo.com/

- UC-1 Test Login form with empty credentials:
1) Type any credentials into "Username" and "Password" fields.
2) Clear the inputs.
3) Hit the "Login" button.
4) Check the error messages: "Username is required".

- UC-2 Test Login form with credentials by passing Username:
1) Type any credentials in username.
2) Enter password.
3) Clear the "Password" input.
4) Hit the "Login" button.
5) Check the error messages: "Password is required".

- UC-3 Test Login form with credentials by passing Username & Password:
1) Type credentials in username which are under Accepted username are sections.
2) Enter password as secret sauce.
3) Click on Login and validate the title “Swag Labs” in the dashboard.

-Additional tasks:
1) Provide parallel execution, add logging for tests and use Data Provider to parametrize tests. Make sure that all tasks are supported by these 3 conditions: UC-1; UC-2; UC-3.

2) Please, add task description as README.md into your solution!

- To perform the task use the various of additional options:
+ Test Automation tool: Selenium WebDriver;
+ Browsers: Firefox; Chrome; 
+ Locators: CSS ;
+ Test Runner: xUnit;

+ [Optional] Patterns: Factory method; Abstract Factory; Chain of responsibility;
+ [Optional] Test automation approach: BDD;
+ Assertions: Fluent Assertion;
+ [Optional] Loggers: SeriLog.

## Requirements
To run this project, make sure you have the following installed:

- .NET SDK (Recommended version: 6.0 or above)
- Selenium WebDriver for browser automation
- ChromeDriver for running tests in the Chrome browser
- GeckoDriver for running tests in the FireFox browser
- EdgeDriver for running tests in the Microsoft Edge browser
- xUnit for running and managing tests
- Chrome Web Browser
- FireFox Web Browser
- Edge Web Browser

## How to Run
1. Clone the repository.
2. Open the solution in Visual Studio or Visual Studio Code.
3. Install dependencies.
4. Run the tests using the built-in test runner or from the command line.

## How to Run the Tests:
To run the tests, you can specify which browser to use (Chrome or Firefox) by providing a browser name in the test parameters.

For example, using `xUnit`'s `InlineData`:
- For Chrome: `InlineData("chrome", ...)`
- For Firefox: `InlineData("firefox", ...)`
- For Edge: `InlineData("edge", ...)`

## Tests
- EmptyFieldsReturnUsernameReq: Test login form with empty credentials.
- EmptyPasswordReturnsPasswordReq: Test login form with a username but missing password.
- CorrectCredReturnsSwagLabs: Test login with valid credentials.

## Logging
This project uses Serilog to log the actions during the tests. Logs are stored in a file located in the logs directory by default. You can configure the logging output in the UnitTests.cs file or by modifying the logger configuration.