using GithubAutomation.Pages;
using NUnit.Framework;

namespace GithubTests.Utilities;

public class IssueTest : BaseSetup
{
    protected static string IssueTitle => TestContext.CurrentContext.Test.Name;

    private static string IssueComment => TestContext.CurrentContext.Test.Name;

    // Refactor: File uploading should be handled by generic method.
    private string FileName = "";

    [SetUp]
    public void Create()
    {
        RepositoryTest.CreatePrivate();
        RepositoriesPage.GoTo();
        RepositoriesPage.SelectRepository(RepositoryTest.Name);

        NewIssuePage.GoTo();
        NewIssuePage.CreateIssue(IssueTitle).WithComment(IssueComment).UploadFile(FileName).Add();
    }

    [TearDown]
    public static void Delete()
    {
        IssuesPage.SelectIssue(IssueTitle);
        IssuePage.DeleteIssue(IssueTitle);

        RepositoryTest.Delete();
    }
}