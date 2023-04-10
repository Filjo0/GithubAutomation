using System;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class NewIssuePage
{
    private static bool IsAt() => Driver.Instance.Url.Contains("/issues/new");

    public static void GoTo()
    {
        if (!IsAt())
        {
            IssuesPage.GoTo();
            Driver.Instance.FindElement(By.XPath("//*[contains(text(), 'New issue')]")).Click();
        }
    }

    public static CreateIssueCommand CreateIssue(string name)
    {
        return new CreateIssueCommand(name);
    }

    public class CreateIssueCommand
    {
        private readonly string _title;
        private string _issueComment;
        private string _filepath;

        public CreateIssueCommand(string title)
        {
            _title = title;
        }

        public CreateIssueCommand WithComment(string issueComment)
        {
            _issueComment = issueComment;
            return this;
        }

        public CreateIssueCommand UploadFile(string fileName)
        {
            _filepath = fileName;
            return this;
        }

        public void Add()
        {
            Driver.Instance.SendText(By.Id("issue_title"), _title);
            Driver.Instance.SendText(By.Id("issue_body"), _issueComment);
            AddFile(_filepath);

            Driver.Wait(TimeSpan.FromSeconds(3));

            Driver.Instance.FindElement(By.XPath("//button[contains(text(), 'Submit new issue')]")).Click();
        }

        private static void AddFile(string fileName)
        {
            if (fileName.Length > 0)
            {
                Driver.Instance.SendText(By.Id("fc-issue_body"), fileName);
            }
        }
    }
}