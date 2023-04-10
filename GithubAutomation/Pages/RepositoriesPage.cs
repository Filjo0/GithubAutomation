using System;
using GithubAutomation.Navigation;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class RepositoriesPage
{
    private static IWebElement Repository(string name) => Driver.Instance.FindElement(By.XPath($"//h3//a[contains(text(), '{name}')]"));

    private static bool IsAt => Driver.Instance.IsElementPresent(By.Id("user-repositories-list"));

    public static void GoTo()
    {
        if (!IsAt)
        {
            NavigationPanel.SelectProfileDropdownItem(ProfileDropdownItem.YourRepositories);
        }
    }

    public static void SelectRepository(string name)
    {
        GoTo();
        Repository(name).Click();
    }

    public static bool IsRepositoryDisplayed(string name)
    {
        GoTo();
        Driver.Instance.SendText(By.Id("your-repos-filter"), name);

        Driver.Wait(TimeSpan.FromSeconds(3));

        return Driver.Instance.IsElementPresent(By.XPath($"//h3//a[contains(text(), '{name}')]"));
    }
}