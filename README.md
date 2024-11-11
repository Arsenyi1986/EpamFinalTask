# Final EPAM assignment

## Description
This project is a test automation suite built with Selenium WebDriver and xUnit to test the login functionality of a web application. It automates the testing of various user login scenarios to ensure correct behavior.



## Requirements
To run this project, make sure you have the following installed:

- .NET SDK (Recommended version: 6.0 or above)
- Selenium WebDriver for browser automation
- ChromeDriver for running tests in the Chrome browser
- xUnit for running and managing tests
- Chrome Web Browser for running the tests

## How to Run
1. Clone the repository.
2. Open the solution in Visual Studio or Visual Studio Code.
3. Run the tests using the built-in test runner or from the command line.

## Tests
- EmptyFieldsReturnUsernameReq: Test login form with empty credentials.
- EmptyPasswordReturnsPasswordReq: Test login form with a username but missing password.
- CorrectCredReturnsSwagLabs: Test login with valid credentials.

## Logging
This project uses Serilog to log the actions during the tests. Logs are stored in a file located in the logs directory by default. You can configure the logging output in the UnitTests.cs file or by modifying the logger configuration.