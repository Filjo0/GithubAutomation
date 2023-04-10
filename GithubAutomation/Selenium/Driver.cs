using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GithubAutomation.Selenium;

public static class Driver
{
    public static IWebDriver Instance { get; private set; }
    public static string BaseAddress => Configuration.Configuration.ParsedData["General"]["BaseAddress"];
    public static readonly string Username = Configuration.Configuration.ParsedData["General"]["Username"];
    public static readonly string Password = Configuration.Configuration.ParsedData["General"]["Password"];

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

    public static void SendText(this IWebDriver driver, By by, string text)
    {
        if (IsElementPresent(driver, by))
        {
            var inputElement = driver.FindElement(by);
            inputElement.Click();
            inputElement.Clear();
            inputElement.SendKeys(text);
        }
    }
}