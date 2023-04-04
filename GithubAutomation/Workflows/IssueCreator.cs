using GithubAutomation.Pages;

namespace GithubAutomation.Workflows;

public static class IssueCreator
{
    public static void CreateIssue()
    {
        // Refactor: Values shouldn't be hardcoded.
        IssuePage.OpenNewIssueWindow();

        IssueTitle = "Test Issue";
        IssueBody = "This is the test body of issue";
            
        // A path to the file needs to be specified.
        // Refactor: Find a better way to upload files.
        IssueFile = "";


        IssuePage
            .CreateIssue(IssueTitle)
            .WithBody(IssueBody)
            .UploadFile(IssueFile)
            .Publish();
    }

    private static string IssueFile { get; set; }

    private static string IssueBody { get; set; }

    public static string IssueTitle { get; private set; }
}