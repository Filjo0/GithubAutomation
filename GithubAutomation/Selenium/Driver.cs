using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GithubAutomation.Selenium;

public static class Driver
{
    public static IWebDriver Instance { get; private set; }

    // Refactor: Add it to the config file.
    public static string BaseAddress => "https://github.com/";

    // Change to your username and to your password.
    // Refactor: it should be added to the config file.
    public const string Username = "?";
    public const string Password = "?";

    public static void Initialize()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("--start-maximized");

        Instance = new ChromeDriver(chromeOptions);
        Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    public static void Close()
    {
        Instance.Quit();
    }

    public static void Wait(TimeSpan timeSpan)
    {
        Thread.Sleep((int)(timeSpan.TotalSeconds * 1000));
    }

    public static void TakeScreenshot()
    {
        var screenshotDriver = (ITakesScreenshot)Instance;
        var screenshot = screenshotDriver.GetScreenshot();
        Instance.Manage().Window.Maximize();
        screenshot.SaveAsFile(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".png", ScreenshotImageFormat.Png);
    }

    public static bool IsElementPresent(this IWebDriver driver, By by)
    {
        try
        {
            driver.FindElement(by);
            return true;
        }
        catch (Exception exception) when (exception is NoSuchElementException or WebDriverTimeoutException)
        {
            return false;
        }
    }
}
