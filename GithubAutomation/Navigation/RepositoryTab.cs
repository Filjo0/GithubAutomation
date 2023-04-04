using System.ComponentModel;

namespace GithubAutomation.Navigation;

public enum RepositoryTab
{
    Code,
    Issues,
    PullRequests,
    Actions,
    Projects,
    Security,
    Insights,
    Settings
}

public static class RepositoryTabExtensions
{
    public static string ToId(this RepositoryTab tab)
    {
        switch (tab)
        {
            case RepositoryTab.Issues:
                return "issues-tab";
            case RepositoryTab.Settings:
                return "settings-tab";
            default:
                throw new InvalidEnumArgumentException("Tab is not recognised.");
        }
    }
}