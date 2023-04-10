using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class IssuePage
{
    public static string GetIssueName() => Driver.Instance.FindElement(By.ClassName("js-issue-title")).Text;

    public static void DeleteIssue(string name)
    {
        IssuesPage.GoTo();
        IssuesPage.SelectIssue(name);

        Driver.Instance.FindElement(By.XPath("//*[contains(@class, 'js-delete-issue')]//summary")).Click();
        Driver.Instance.FindElement(By.XPath("//button[contains(text(), 'Delete this issue')]")).Click();
    }
}