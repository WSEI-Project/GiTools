using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiTools.Services.Interfaces
{
    interface IGitService
    {
        Task CreateCommit(long repoId, string directoryToAdd, string commitText);
        Task<long> CreateRepo(string repoName, bool isPrivate);
        Task<IReadOnlyList<RepositoryContributor>> GetContributors(long repoId);
        Task<SearchRepositoryResult> SearchRepositories(SearchRepositoriesRequest req);
        Task<CommitActivity> GetCommitActivity(long repoId);
        Task DownloadRepo(long repoId, string path);
        Task<CodeFrequency> GetCodeFrequency(long repoId);
        Task<IReadOnlyList<Repository>> GetUsersRepo();
        bool AuthenticateUser(string token);
    }
}
