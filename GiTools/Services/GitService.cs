using Octokit;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace GiTools.Services
{
    public class GitService
    {
        private static readonly string GitHubIdentity = Assembly
            .GetEntryAssembly()
            .GetCustomAttribute<AssemblyProductAttribute>()
            .Product;

        public async Task CreateCommit(string owner, string repo, string directoryToAdd, string commitText)
        {
            var headMasterRef = "heads/master";
            var github = GetClient("token");
            var masterReference = await github.Git.Reference.Get(owner, repo, headMasterRef);
            var latestCommit = await GetLatestSHA(owner, repo, headMasterRef);

            var nt = new NewTree { BaseTree = latestCommit.Tree.Sha };
            string[] filePaths = Directory.GetFiles(string.Format(@"{0}\", directoryToAdd));

            foreach (var filePath in filePaths)
            {
                var linesOfCode =  await File.ReadAllLinesAsync(filePath);
                
                var newTreeItem = new NewTreeItem { Mode = "100644", Type = TreeType.Blob, Content = linesOfCode.ToString() , Path = filePath };
                nt.Tree.Add(newTreeItem);
            }

            var newTree = await github.Git.Tree.Create(owner, repo, nt);
            var newCommit = new NewCommit(commitText, newTree.Sha, masterReference.Object.Sha);
            var commit = await github.Git.Commit.Create(owner, repo, newCommit);
            await github.Git.Reference.Update(owner, repo, headMasterRef, new ReferenceUpdate(commit.Sha));

        }
        public async Task CreateRepo(string repoName, bool isPrivate)
        {
            var github = GetClient("token");
            await github.Repository.Create(new NewRepository(repoName) { Private = isPrivate });
        }
        private async Task<Commit> GetLatestSHA(string owner, string repo, string headMasterRef)
        {
            var github = GetClient("token");
            var masterReference = await github.Git.Reference.Get(owner, repo, headMasterRef);
            // Get the laster commit of this branch
            return await github.Git.Commit.Get(owner, repo, masterReference.Object.Sha);
        }

        private static GitHubClient GetClient(string token)
        {
            var client = new GitHubClient(new ProductHeaderValue(GitHubIdentity));
            client.Credentials = new Credentials(token);

            return client;
        }
    }
}
