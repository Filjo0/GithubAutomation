﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using GithubAutomation.Navigation;
using GithubAutomation.Selenium;
using OpenQA.Selenium;

namespace GithubAutomation.Pages;

public static class IssuePage
{
    private static readonly IWebElement SubmitDeleteButton = Driver.Instance.FindElement(By.CssSelector("button[name='verify_delete']"));

    private static void SubmitDelete() => SubmitDeleteButton.Click();

    public static string Title
    {
        get
        {
            var title = Driver.Instance.FindElement(By.CssSelector("h1.break-word.f1"));
            return title != null ? title.Text : string.Empty;
        }
    }

    public static int PreviousIssueCount { get; private set; }

    public static int CurrentIssueCount => GetIssueCount();

    public static void GoToListOfIssues()
    {
        RepoPanel.Issues.Select();
    }

    public static bool IsAt()
    {
        return IssuePanel.AddIssue.GetText().Contains("New issue");
    }

    public static void OpenNewIssueWindow()
    {
        IssuePanel.AddIssue.Select();
    }

    public static CreateIssueCommand CreateIssue(string title)
    {
        return new CreateIssueCommand(title);
    }

    public static void CountIssues()
    {
        PreviousIssueCount = GetIssueCount();
    }

    private static int GetIssueCount()
    {
        var number = 0;
        var countIssues = Driver.Instance.FindElement(By.CssSelector("[data-tab-item='i1issues-tab'] span.Counter"));
        if (countIssues.Displayed)
        {
            number = Convert.ToInt32(countIssues.Text);
        }

        return number;
    }

    public static bool? DoesIssueExistWithTitle(string title)
    {
        return Driver.Instance.FindElements(By.LinkText(title)).Any();
    }

    public static void DeleteIssue(string title)
    {
        var issuesWithTitle = Driver.Instance.FindElements(By.LinkText(title));
        if (issuesWithTitle.Count > 0)
        {
            issuesWithTitle[0].Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
        }

        var buttons = Driver.Instance.FindElements(By.CssSelector("summary span strong"));
        foreach (var button in buttons)
        {
            if (button.Text.Contains("Delete"))
            {
                button.Click();
            }
        }


        if (SubmitDeleteButton.Displayed)
        {
            SubmitDelete();
        }
    }

    public static bool FoundOpenIssues()
    {
        var openIssues = Driver.Instance.FindElement(By.CssSelector("div.flex-auto.d-none.d-lg-block.no-wrap > div > a.btn-link.selected")).Text;
        var openIssuesNum = int.Parse(Regex.Replace(openIssues, "[^0-9]+", ""));
        return openIssuesNum > 0;
    }

    public static void SearchForIssue(string title)
    {
        var searchBox = Driver.Instance.FindElement(By.Id("js-issues-search"));
        searchBox.SendKeys(title);
        searchBox.SendKeys(Keys.Enter);
        Driver.Wait(TimeSpan.FromSeconds(1));
    }
}

public class CreateIssueCommand
{
    private static void EnterTitle(string title) => Driver.Instance.FindElement(By.Id("issue_title")).SendKeys(title);

    private static void EnterBody(string body) => Driver.Instance.FindElement(By.Id("issue_body")).SendKeys(body);

    private static void EnterPath(string filepath) => Driver.Instance.FindElement(By.Id("fc-issue_body")).SendKeys(filepath);

    private static void Submit() => Driver.Instance.FindElement(By.CssSelector(".flex-items-center button.btn-primary.btn")).Click();


    private readonly string _title;
    private string _issueBody;
    private string _filepath;

    public CreateIssueCommand(string title)
    {
        _title = title;
    }


    public CreateIssueCommand WithBody(string issueBody)
    {
        _issueBody = issueBody;
        return this;
    }

    public CreateIssueCommand UploadFile(string filepath)
    {
        _filepath = filepath;
        return this;
    }

    public void Publish()
    {
        EnterTitle(_title);
        EnterBody(_issueBody);
        if (_filepath.Length > 0)
        {
            EnterPath(_filepath);
        }

        Driver.Wait(TimeSpan.FromSeconds(5));

        Submit();
    }
}