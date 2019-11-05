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
        public void CreateCommit(string owner,string repo, string branch)
        {
            github = GetClient(new ProductHeaderValue(GitHubIdentity), "token");

        }

        private async Task<Commit> GetLatestSHA(string owner, string repo)
        {
            var headMasterRef = "heads/master";
            // Get reference of master branch
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
