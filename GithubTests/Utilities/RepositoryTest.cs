using GithubAutomation.Pages;
using NUnit.Framework;

namespace GithubTests.Utilities;

public class RepositoryTest : BaseSetup
{
    protected static string Name => TestContext.CurrentContext.Test.Name;
    private static string Description => TestContext.CurrentContext.Test.Name;
    private const bool IsPrivate = true;

    [SetUp]
    public static void Create()
    {
        NewRepoPage.GoTo();
        NewRepoPage.CreateRepo(Name).WithDescription(Description).IsPrivate(IsPrivate).Publish();
    }

    [TearDown]
    public static void Delete()
    {
        RepositoriesPage.SelectRepository(Name);
        RepositoryPage.DeleteRepository(Name);
    }
}