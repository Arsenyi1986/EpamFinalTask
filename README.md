# Final EPAM assignment

## Description
This project is a test automation suite built with Selenium WebDriver and xUnit to test the login functionality of a web application. It automates the testing of various user login scenarios to ensure correct behavior.

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