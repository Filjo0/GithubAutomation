using System;
using GithubAutomation.Navigation;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class NewRepoPage
{
    private static bool IsAt() => Driver.Instance.IsElementPresent(By.XPath("//h1[contains(text(), 'Create a new repository')]"));
    
    public static void GoTo()
    {
        if (!IsAt())
        {
           NavigationPanel.SelectNewDropdownItem(NewDropdownItem.NewRepository);
        }
    }

    public static CreatRepoCommand CreateRepo(string name)
    {
        return new CreatRepoCommand(name);
    }

    public class CreatRepoCommand
    {
        private readonly string _repositoryName;
        private string _repositoryDescription;
        private bool _isPrivate;

        private static void EnterName(string name) => Driver.Instance.FindElement(By.Id("repository_name")).SendKeys(name);
        private static void EnterDescription(string description) => Driver.Instance.FindElement(By.Id("repository_description")).SendKeys(description);
        private static void SetVisibilityToPrivate() => Driver.Instance.FindElement(By.Id("repository_visibility_private")).Click();

        public CreatRepoCommand(string repositoryName)
        {
            _repositoryName = repositoryName;
        }

        public CreatRepoCommand WithDescription(string description)
        {
            _repositoryDescription = description;
            return this;
        }

        public CreatRepoCommand IsPrivate(bool isPrivate)
        {
            _isPrivate = isPrivate;
            return this;
        }

        public void Publish()
        {
            EnterName(_repositoryName);
            EnterDescription(_repositoryDescription);
            if (_isPrivate)
            {
                SetVisibilityToPrivate();
            }

            Driver.Wait(TimeSpan.FromSeconds(1));
            Driver.Instance.FindElement(By.XPath("//button[contains(text(), 'Create repository')]")).Click();
        }
    }
}