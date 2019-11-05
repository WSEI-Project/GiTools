using Octokit;
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
        private GitHubClient github;
        public async Task CreateCommit(string owner,string repo)
        {
            var headMasterRef = "heads/master";
            github = GetClient(new ProductHeaderValue(GitHubIdentity), "token");
            var masterReference = await github.Git.Reference.Get(owner, repo, headMasterRef);
            var latestCommit = await GetLatestSHA(owner, repo, headMasterRef);
            var textBlobRef = await CreateTextBlob(owner, repo, "/path/to/file");

            var nt = new NewTree { BaseTree = latestCommit.Tree.Sha };
            var newTreeItem = new NewTreeItem { Mode = "100644", Type = TreeType.Blob, Content = "file.content", Path = "path/to/file.txt" };
            nt.Tree.Add(newTreeItem);

            var newTree = await github.Git.Tree.Create(owner, repo, nt);
            var newCommit = new NewCommit("Commit test with several files", newTree.Sha, masterReference.Object.Sha);
            var commit = await github.Git.Commit.Create(owner, repo, newCommit);
            await github.Git.Reference.Update(owner, repo, headMasterRef, new ReferenceUpdate(commit.Sha));
        }
        private async Task<BlobReference> CreateTextBlob(string owner, string repo, string pathToFile)
        {
            var textBlob = new NewBlob { Encoding = EncodingType.Utf8, Content = "Hellow World!" };
            return await github.Git.Blob.Create(owner, repo, textBlob);
        }
        private async Task<Commit> GetLatestSHA(string owner, string repo, string headMasterRef)
        {
            var masterReference = await github.Git.Reference.Get(owner, repo, headMasterRef);
            // Get the laster commit of this branch
            return await github.Git.Commit.Get(owner, repo, masterReference.Object.Sha);
        }

        private static GitHubClient GetClient(ProductHeaderValue productInformation,
            string token)
        {
            var client = new GitHubClient(new ProductHeaderValue(GitHubIdentity));
            client.Credentials = new Credentials("personalToken");

            return client;
        }
    }
}
