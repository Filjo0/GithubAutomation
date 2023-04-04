using GithubAutomation.Pages;
using GithubTests.Utilities;
using NUnit.Framework;

namespace GithubTests.Tests;

public class RepositoryTests : RepositoryTest
{
    /// <summary>
    /// Checks that a repository can be added.
    /// </summary>
    [Test]
    public void Can_Create_Repo()
    {
        Assert.That(RepositoriesPage.IsRepositoryFound(RepositoryTest.Name), Is.True, "Failed to locate added repository.");
    }

    /// <summary>
    /// Checks that specific repository can be open.
    /// </summary>
    [Test]
    public void Can_Open_Repo()
    {
        RepositoriesPage.SelectRepository(RepositoryTest.Name);
        Assert.That(RepositoryPage.GetRepoName(), Is.EqualTo(RepositoryTest.Name), "Failed to locate the repository.");
    }
}