using System;
using GithubAutomation.Navigation;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class IssuesPage
{
    private static IWebElement Issue(string name) => Driver.Instance.FindElement(By.XPath($"//a[contains(@data-hovercard-type, 'issue') and contains(text(), '{name}')]"));

    private static bool IsAt() => Driver.Instance.IsElementPresent(By.Id("filters-select-menu"));

    /// <summary>
    /// Navigates to the Issues page.
    /// </summary>
    public static void GoTo()
    {
        if (!IsAt())
        {
            RepositoryPage.SelectTab(RepositoryTab.Issues);
        }
    }

    /// <summary>
    /// Checks if there are any issues in the current repository.
    /// </summary>
    /// <returns></returns>
    public static bool HaveIssues()
    {
        GoTo();
        var totalRepos = Driver.Instance.FindElement(By.XPath("//[@data-tab-item='i1issues-tab]//span")).GetCssValue("title");
        return totalRepos != "0";
    }

    /// <summary>
    /// Selects an issue with the given name.
    /// </summary>
    /// <param name="name"></param>
    public static void SelectIssue(string name)
    {
        GoTo();
        Issue(name).Click();
    }

    /// <summary>
    /// Checks if an issue with the given name is displayed on the issues page.
    /// </summary>
    /// <param name="name">The name of the issue.</param>
    /// <returns>True if the issue is displayed.</returns>
    public static bool IsIssueDisplayed(string name)
    {
        GoTo();
        Driver.Instance.SendText(By.Id("js-issues-search"), name);

        Driver.Wait(TimeSpan.FromSeconds(3));

        return Driver.Instance.IsElementPresent(By.XPath($"//a[contains(@id, 'issue_1_link') and text()='{name}']"));
    }
}