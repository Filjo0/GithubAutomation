using GithubAutomation.Pages;
using GithubAutomation.Workflows;
using GithubTests.Utilities;
using NUnit.Framework;

namespace GithubTests.Tests;

[TestFixture]
public class NewIssuesTest : BaseSetup
{
    /// <summary>
    /// Checks that the issues page can be opened.
    /// </summary>
    [Test]
    public void Can_Go_To_List_Of_Issues_Page()
    {
        // Refactor: Find a better way to go to the page.
        RepoPage.GoToRepoPage("TestRepo");
        IssuePage.GoToListOfIssues();
        Assert.That(IssuePage.IsAt(), "Failed to open the Issues page");
    }

    /// <summary>
    /// Checks that new issue can be added.
    /// </summary>
    [Test]
    public void Can_Create_Issue()
    {
        // Refactor: Find a better way to go to the page.
        RepoPage.GoToRepoPage("TestRepo");
        IssuePage.GoToListOfIssues();

        IssueCreator.CreateIssue();
        IssuePage.GoToListOfIssues();
        Assert.IsTrue(IssuePage.DoesIssueExistWithTitle(IssueCreator.IssueTitle), "Failed to locate added issue..");
    }

    /// <summary>
    /// Checks that added issue is visible in the list.
    /// </summary>
    [Test]
    public void Added_Issue_Shows_Up()
    {
        // Refactor: Find a better way to go to the page.
        RepoPage.GoToRepoPage("TestRepo");
            
        IssuePage.GoToListOfIssues();
        IssuePage.CountIssues();
            
        IssueCreator.CreateIssue();

        IssuePage.GoToListOfIssues();
        Assert.AreEqual(IssuePage.PreviousIssueCount + 1, IssuePage.CurrentIssueCount, "Failed to add new issue to the list.");
        Assert.IsTrue(IssuePage.DoesIssueExistWithTitle("Test Issue"), "Failed to locate added issue.");

        // Refactor: This can be implemented in the workflow class.
        IssuePage.DeleteIssue(IssueCreator.IssueTitle);
            
        // Refactor: It should be a separate test case.
        Assert.AreEqual(IssuePage.PreviousIssueCount, IssuePage.CurrentIssueCount, "Failed to delete added issue.");
    }

    /// <summary>
    /// Checks that particular issue can be found when using search.
    /// </summary>
    [Test]
    public void Can_Search_Issues()
    {
        // Refactor: Find a better way to go to the page.
        RepoPage.GoToRepoPage("TestRepo");
        IssuePage.GoToListOfIssues();

        IssuePage.SearchForIssue("Test Issue");

        // Refactor: There should be no if in the test.
        if (!IssuePage.FoundOpenIssues())
        {
            IssueCreator.CreateIssue();
            IssuePage.GoToListOfIssues();
        }

        IssuePage.DeleteIssue("Test Issue");
            
        // Refactor: Should be checking if the issue is found, then clean up.
        Assert.IsFalse(IssuePage.DoesIssueExistWithTitle("Test Issue"), "Failed to delete the issue.");
    }
}