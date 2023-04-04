using System;
using System.Reflection;
using GithubAutomation.Pages;
using GithubAutomation.Selenium;
using log4net;
using log4net.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace GithubTests.Utilities;

[TestFixture]
public class BaseSetup
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    [OneTimeSetUp]
    public void Initialize()
    {
        try
        {
            Driver.Initialize();
            XmlConfigurator.Configure();

            LoginPage.GoTo();
            LoginPage
                .LoginAs(Driver.Username)
                .WithPassword(Driver.Password)
                .Login();
            
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message + exception.StackTrace);

            throw;
        }
    }

    [OneTimeTearDown]
    public void CleanUp()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            Log.Error("Failed to test " + TestContext.CurrentContext.Test.Name);
            Driver.TakeScreenshot();
            Driver.Close();
        }

        Driver.Close();
    }
}