using GithubAutomation.Pages;
using GithubTests.Utilities;
using NUnit.Framework;

namespace GithubTests.Tests;

public class IssueTests : IssueTest
{
    /// <summary>
    /// Checks that an issue can be created.
    /// </summary>
    [Test]
    public void Can_Create_Issue()
    {
        Assert.That(IssuesPage.IsIssueDisplayed(IssueTitle), Is.True, "Failed to locate created issue.");
    }

    /// <summary>
    /// Checks that specific issue can be open.
    /// </summary>
    [Test]
    public void Can_Open_Issue()
    {
        IssuesPage.SelectIssue(IssueTitle);
        Assert.That(IssuePage.GetIssueName(), Is.EqualTo(IssueTitle), "Failed to locate the issue.");
    }
}