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
        RepositoriesPage.GoToListOfRepos();
        RepositoriesPage.SelectRepository(name);

        SelectTab(RepositoryTab.Settings);

        Driver.Instance.FindElement(By.XPath("//summary[contains(text(), 'Delete this')]")).Click();
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
        Driver.Instance.FindElement(By.XPath("//input[contains(@aria-label, 'delete this repository')]")).SendKeys(Driver.Username + "/" + name);
        Driver.Instance.FindElement(By.XPath("//button//span[contains(text(), 'delete this repository')]")).Click();
    }
}