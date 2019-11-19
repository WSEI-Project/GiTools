using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiTools.Services.Interfaces
{
    interface IGitService
    {
        Task CreateCommit(string owner, string repo, string directoryToAdd, string commitText);
        Task CreateRepo(string repoName, bool isPrivate);
        Task<IReadOnlyList<RepositoryContributor>> GetContributors(long repoId);
        Task<SearchRepositoryResult> SearchRepositories(SearchRepositoriesRequest req);
        Task<CommitActivity> GetCommitActivity(long repoId);
        Task DownloadRepo(string owner, string name, string path);
    }
}
