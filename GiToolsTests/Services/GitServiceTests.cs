using Xunit;
using GiTools.Services;
namespace GiToolsTests.Services
{
    public class GitServiceTests
    {
        /*
        public GitService(string token)
        public async Task CreateCommit(string owner, string repo, string directoryToAdd, string commitText)
        public async Task CreateRepo(string repoName, bool isPrivate)
        public async Task<SearchRepositoryResult> SearchRepositories(SearchRepositoriesRequest req)
        public async Task<CommitActivity> GetCommitActivity(long repoId)
        */
        #region DownloadRepo
            //not implemented yet
        #endregion
        #region CodeFrequency
        [Fact]
        public async void TestGetCodeFrequency()
        {
            var git = new GitService(GitTestsToken.MinimalPermissions);
            var frequency = await git.GetCodeFrequency(GitTestRepository.Public);
            Assert.IsType<Octokit.CodeFrequency>(frequency);
        }
        [Fact]
        public async void TestGetCodeFrequencyException()
        {
            //incorrect repo
            long incorrectRepoId = -1;
            var git = new GitService(GitTestsToken.MinimalPermissions);
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await git.GetCodeFrequency(incorrectRepoId);
            });
        }
        [Fact]
        public async void TestGetCodeFrequencyPrivateRepoAccessible()
        {
            var git = new GitService(GitTestsToken.FullPermissions);
            var frequency = await git.GetCodeFrequency(GitTestRepository.Private);
            Assert.IsType<Octokit.CodeFrequency>(frequency);
        }
        [Fact]
        public async void TestGetCodeFrequencyPrivateRepoNotAccessible()
        {
            var git = new GitService(GitTestsToken.MinimalPermissions);
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await git.GetCodeFrequency(GitTestRepository.Private);
            });
        }
        #endregion
        #region GetContributors
        [Fact]
        public async void TestGetContributors()
        {
            var git = new GitService(GitTestsToken.MinimalPermissions);
            var contributors = await git.GetContributors(GitTestRepository.Public);
            Assert.True(contributors.Count > 0);
        }
        [Fact]
        public async void TestGetContributorsException()
        {
            //incorrect repo
            long incorrectRepoId = -1;
            var git = new GitService(GitTestsToken.MinimalPermissions);
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await git.GetContributors(incorrectRepoId);
            });
        }
        [Fact]
        public async void TestGetContributorsPrivateRepoAccessible()
        {
            var git = new GitService(GitTestsToken.FullPermissions);
            var contributors = await git.GetContributors(GitTestRepository.Private);
            Assert.True(contributors.Count > 0);
        }
        [Fact]
        public async void TestGetContributorsPrivateRepoNotAccessible()
        {
            var git = new GitService(GitTestsToken.MinimalPermissions);
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await git.GetContributors(GitTestRepository.Private);
            });
        }
        #endregion
    }
}
