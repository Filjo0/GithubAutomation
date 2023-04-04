using System;
using GithubAutomation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GithubAutomation.Pages;

public static class LoginPage
{
    private static bool IsAt => Driver.Instance.Url.Contains("login");

    public static void GoTo()
    {
        if (!IsAt)
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress + "login");
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(d => d.SwitchTo().ActiveElement().GetAttribute("id") == "login_field");
        }
    }

    public static LoginCommand LoginAs(string username)
    {
        return new LoginCommand(username);
    }

    public class LoginCommand
    {
        private readonly string _username;
        private string _password;


        public LoginCommand(string username)
        {
            _username = username;
        }

        public LoginCommand WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public void Login()
        {
            Driver.Instance.FindElement(By.Id("login_field")).SendKeys(_username);
            Driver.Instance.FindElement(By.Id("password")).SendKeys(_password);
            Driver.Instance.FindElement(By.ClassName("js-sign-in-button")).Click();
        }
    }
}