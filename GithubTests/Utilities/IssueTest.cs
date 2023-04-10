using GithubAutomation.Pages;
using NUnit.Framework;

namespace GithubTests.Utilities;

public class IssueTest : BaseSetup
{
    protected static readonly string IssueTitle = TestContext.CurrentContext.Test.Name;
    private static readonly string IssueComment = TestContext.CurrentContext.Test.Name;
    private const string FileName = "";

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