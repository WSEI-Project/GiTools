using GiTools.Services.Interfaces;
using Octokit;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;

namespace GiTools.Services
{
    public class GitService : IGitService
    {
        private static string _token;
        private static readonly string GitHubIdentity = Assembly
            .GetEntryAssembly()
            .GetCustomAttribute<AssemblyProductAttribute>()
            .Product;
        public GitService(string token)
        {
            _token = token;
        }
        public async Task CreateCommit(long repoId, string directoryToAdd, string commitText)
        {
            var headMasterRef = "heads/master";
            var github = GetClient();
            var masterReference = await github.Git.Reference.Get(repoId, headMasterRef);
            var latestCommit = await GetLatestSHA(repoId, headMasterRef);

            var nt = new NewTree { BaseTree = latestCommit.Tree.Sha };
            string[] filePaths = Directory.GetFiles(string.Format(@"{0}\", directoryToAdd));

            foreach (var filePath in filePaths)
            {
                var linesOfCode = await File.ReadAllLinesAsync(filePath);

                var newTreeItem = new NewTreeItem { Mode = "100644", Type = TreeType.Blob, Content = linesOfCode.ToString(), Path = filePath };
                nt.Tree.Add(newTreeItem);
            }

            var newTree = await github.Git.Tree.Create(repoId, nt);
            var newCommit = new NewCommit(commitText, newTree.Sha, masterReference.Object.Sha);
            var commit = await github.Git.Commit.Create(repoId, newCommit);
            await github.Git.Reference.Update(repoId, headMasterRef, new ReferenceUpdate(commit.Sha));

        }
        public async Task<long> CreateRepo(string repoName, bool isPrivate)
        {
            var github = GetClient();
            var repo = await github.Repository.Create(new NewRepository(repoName) { Private = isPrivate });
            return repo.Id;
        }
        public async Task<IReadOnlyList<RepositoryContributor>> GetContributors(long repoId)
        {
            var github = GetClient();
            return await github.Repository.GetAllContributors(repoId);
        }
        public async Task<SearchRepositoryResult> SearchRepositories(SearchRepositoriesRequest req)
        {
            var github = GetClient();
            return await github.Search.SearchRepo(req);
        }
        public async Task<CommitActivity> GetCommitActivity(long repoId)
        {
            var github = GetClient();
            return await github.Repository.Statistics.GetCommitActivity(repoId);
        }
        public async Task<CodeFrequency> GetCodeFrequency(long repoId)
        {
            var github = GetClient();
            return await github.Repository.Statistics.GetCodeFrequency(repoId);
        }
        public async Task<IReadOnlyList<Repository>> GetUsersRepo()
        {
            var github = GetClient();
            return await github.Repository.GetAllForCurrent();
        }
        public async Task DownloadRepo(long repoId, string path)
        {
            var github = GetClient();
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            var zip = await github.Repository.Content.GetArchive(repoId, ArchiveFormat.Zipball);
            string repoName = (await github.Repository.Get(repoId)).Name;
            File.WriteAllBytes(path + repoName, zip);

            string pathWithRepoName = string.Format("{0}{1}", path, repoName);
         
            ZipFile.ExtractToDirectory(pathWithRepoName, path);
            System.IO.File.Delete(pathWithRepoName);
        }
        public async Task<long> GetRepoId(string url)
        {
            var github = GetClient();
            string user = url.Split("/")[3];
            string repoName = url.Split("/")[4];
            if (repoName.EndsWith(".git")) repoName = repoName.Replace(".git", "");
            return (await github.Repository.Get(user, repoName)).Id;
        }
        private async Task<Commit> GetLatestSHA(long repoId, string headMasterRef)
        {
            var github = GetClient();
            var masterReference = await github.Git.Reference.Get(repoId, headMasterRef);
            // Get the laster commit of this branch
            return await github.Git.Commit.Get(repoId, masterReference.Object.Sha);
        }

        private static GitHubClient GetClient()
        {
            return new GitHubClient(new ProductHeaderValue(GitHubIdentity)) { Credentials = new Credentials(_token) };
        }
    }
}
