using GithubService.Entites;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubService
{
    public interface IGitHubService
    {
        Task<int> GetUserFollowersAsync();
        Task<List<Repository>> SearchRepositoriesInCSharp();
        Task<Portfolio> GetPortfolio();
        Task<List<RepositoryDto>> SearchRepositories(string repoName = "", string language = "", string username = "");
    }
}

