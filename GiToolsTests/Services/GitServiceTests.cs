using Xunit;
using GiTools.Services;
namespace GiToolsTests.Services
{
    public class GitServiceTests
    {
        private GitService fullGit;
        private GitService minimalGit;
        public GitServiceTests()
        {
            minimalGit = new GitService(GitTestsToken.MinimalPermissions);
            fullGit = new GitService(GitTestsToken.FullPermissions);
        }

        /*
        public GitService(string token)
        public async Task CreateCommit(string owner, string repo, string directoryToAdd, string commitText)
        public async Task CreateRepo(string repoName, bool isPrivate)
        public async Task<SearchRepositoryResult> SearchRepositories(SearchRepositoriesRequest req)
        */
        #region SearchRepositories
        [Fact]
        public async void TestSearchRepositories()
        {
            var req = new Octokit.SearchRepositoriesRequest("TestRepo");
            Assert.IsType<Octokit.SearchRepositoryResult>(await minimalGit.SearchRepositories(req));
        }
        #endregion
        #region GetCommitActivity
        [Fact]
        public async void TestGetCommitActivity()
        {
            var activity = await minimalGit.GetCommitActivity(GitTestRepository.Public);
            Assert.IsType<Octokit.CommitActivity>(activity);
        }
        [Fact]
        public async void TestGetCommitActivityException()
        {
            //incorrect repo
            long incorrectRepoId = -1;
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await minimalGit.GetCommitActivity(incorrectRepoId);
            });
        }
        [Fact]
        public async void TestGetCommitActivityPrivateRepoAccessible()
        {
            var frequency = await fullGit.GetCommitActivity(GitTestRepository.Private);
            Assert.IsType<Octokit.CommitActivity>(frequency);
        }
        [Fact]
        public async void TestGetCommitActivityPrivateRepoNotAccessible()
        {
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await minimalGit.GetCommitActivity(GitTestRepository.Private);
            });
        }
        #endregion
        #region GetRepoId
        [Fact]
        public async void TestGetRepoId()
        {
            Assert.Equal(GitTestRepository.Public,await minimalGit.GetRepoId(GitTestRepository.PublicUrl));
            Assert.Equal(GitTestRepository.Public, await minimalGit.GetRepoId(GitTestRepository.PublicCloneUrl));
        }
        [Fact]
        public async void TestGetRepoIdPrivateRepoAccessible()
        {
            Assert.Equal(GitTestRepository.Private, await fullGit.GetRepoId(GitTestRepository.PrivateUrl));
            Assert.Equal(GitTestRepository.Private, await fullGit.GetRepoId(GitTestRepository.PrivateCloneUrl));
        }
        [Fact]
        public async void TestGetRepoIdPrivateRepoNotAccessible()
        {
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                Assert.Equal(GitTestRepository.Private, await minimalGit.GetRepoId(GitTestRepository.PrivateUrl));
            });
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                Assert.Equal(GitTestRepository.Private, await minimalGit.GetRepoId(GitTestRepository.PrivateCloneUrl));
            });
            
        }
        #endregion
        #region DownloadRepo
        //not implemented yet
        #endregion
        #region CodeFrequency
        [Fact]
        public async void TestGetCodeFrequency()
        {
            var frequency = await minimalGit.GetCodeFrequency(GitTestRepository.Public);
            Assert.IsType<Octokit.CodeFrequency>(frequency);
        }
        [Fact]
        public async void TestGetCodeFrequencyException()
        {
            //incorrect repo
            long incorrectRepoId = -1;
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await minimalGit.GetCodeFrequency(incorrectRepoId);
            });
        }
        [Fact]
        public async void TestGetCodeFrequencyPrivateRepoAccessible()
        {
            var frequency = await fullGit.GetCodeFrequency(GitTestRepository.Private);
            Assert.IsType<Octokit.CodeFrequency>(frequency);
        }
        [Fact]
        public async void TestGetCodeFrequencyPrivateRepoNotAccessible()
        {
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await minimalGit.GetCodeFrequency(GitTestRepository.Private);
            });
        }
        #endregion
        #region GetContributors
        [Fact]
        public async void TestGetContributors()
        {
            var contributors = await minimalGit.GetContributors(GitTestRepository.Public);
            Assert.True(contributors.Count > 0);
        }
        [Fact]
        public async void TestGetContributorsException()
        {
            //incorrect repo
            long incorrectRepoId = -1;
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await minimalGit.GetContributors(incorrectRepoId);
            });
        }
        [Fact]
        public async void TestGetContributorsPrivateRepoAccessible()
        {
            var contributors = await fullGit.GetContributors(GitTestRepository.Private);

            Assert.True(contributors.Count > 0);
        }
        [Fact]
        public async void TestGetContributorsPrivateRepoNotAccessible()
        {
            await Assert.ThrowsAsync<Octokit.NotFoundException>(async delegate ()
            {
                await minimalGit.GetContributors(GitTestRepository.Private);

            });
        }
        #endregion
    }
}
