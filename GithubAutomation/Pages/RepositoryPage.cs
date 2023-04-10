using System;
using GithubAutomation.Navigation;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class RepositoryPage
{
    public static string GetRepoName() => Driver.Instance.FindElement(By.XPath("//strong[@itemprop='name']//a")).Text;

    public static bool DeleteRepository(string name)
    {
        RepositoriesPage.GoTo();
        RepositoriesPage.SelectRepository(name);

        SelectTab(RepositoryTab.Settings);

        Driver.Instance.FindElement(By.ClassName("js-repo-delete-button")).Click();
        ConfirmDeletion(name);

        return Driver.Instance.FindElement(By.XPath("//div[contains(text(), 'was successfully deleted.')]")).Displayed;
    }

    public static void SelectTab(RepositoryTab tab)
    {
        Driver.Instance.FindElement(By.Id($"{tab.ToId()}")).Click();
        Driver.Wait(TimeSpan.FromSeconds(3));
    }

    private static void ConfirmDeletion(string name)
    {
        Driver.Instance.FindElement(By.ClassName("js-repo-delete-proceed-button")).Click();
        
        Driver.Instance.FindElement(By.XPath("//*[contains(text(), 'I have read and understand these effects')]")).Click();
        
        Driver.Instance.SendText(By.XPath("//input[contains(@id, 'verification_field')]"), Driver.Username + "/" + name);
        Driver.Instance.FindElement(By.XPath("//div[contains(@class, 'full-button')]//span[contains(text(), 'Delete this repository')]")).Click();
    }
}