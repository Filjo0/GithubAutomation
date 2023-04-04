using System;
using GithubAutomation.Navigation;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class RepositoriesPage
{
    private static IWebElement Repository(string name) => Driver.Instance.FindElement(By.XPath($"//h3//a[contains(text(), '{name}')]"));

    private static bool IsAt()
    {
        return Driver.Instance.IsElementPresent(By.Id("user-repositories-list"));
    }

    public static void GoToListOfRepos()
    {
        if (!IsAt())
        {
            NavigationPanel.SelectProfileDropdownItem(ProfileDropdownItem.YourRepositories);
        }
    }

    public static bool HaveRepos()
    {
        GoToListOfRepos();
        var totalRepos = Driver.Instance.FindElement(By.XPath("//[@data-tab-item='repositories]//span")).GetCssValue("title");
        return totalRepos != "0";
    }

    public static bool IsRepositoryFound(string name)
    {
        FindRepository(name);

        return DoesRepoExist(name);
    }

    public static void SelectRepository(string name)
    {
        GoToListOfRepos();
        Repository(name).Click();
    }

    public static void FindRepository(string name)
    {
        GoToListOfRepos();
        Driver.Instance.FindElement(By.Id("your-repos-filter")).SendKeys(name);

        Driver.Wait(TimeSpan.FromSeconds(3));
    }

    private static bool DoesRepoExist(string name)
    {
        return Repository(name).Displayed;
    }
}