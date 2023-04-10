using GithubAutomation.Pages;
using NUnit.Framework;

namespace GithubTests.Utilities;

public class RepositoryTest : BaseSetup
{
    public static string Name => TestContext.CurrentContext.Test.Name;
    private static string Description => TestContext.CurrentContext.Test.Name;
    private const bool IsPrivate = true;

    [SetUp]
    public static void CreatePrivate()
    {
        NewRepoPage.GoTo();
        NewRepoPage.CreateRepo(Name).WithDescription(Description).IsPrivate(IsPrivate).Add();
    }

    [TearDown]
    public static void Delete()
    {
        RepositoriesPage.SelectRepository(Name);
        RepositoryPage.DeleteRepository(Name);
    }
}