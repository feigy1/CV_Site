using GithubService.Entites;
using Microsoft.Extensions.Options;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GithubService
{
    public class GitHubService1 : IGitHubService
    {
        private readonly GitHubClient _gitHubClient;

        private readonly GitHubSetting _options;
        public GitHubService1(IOptions<GitHubSetting> options)
        {
            _options = options.Value;
            _gitHubClient = new GitHubClient(new Octokit.ProductHeaderValue("WebApi"))
            {
                Credentials =new Credentials(_options.Token)
            };
            
        }
        public async Task<int> GetUserFollowersAsync()
        {
            var user = await _gitHubClient.User.Get(_options.UserName);
            return user.Followers;
        }
        public async Task<List<Repository>> SearchRepositoriesInCSharp()
        {
            var request = new SearchRepositoriesRequest("repo-name") { Language = Language.CSharp };
            var result = await _gitHubClient.Search.SearchRepo(request);
            return result.Items.ToList();
        }
        public async Task<Portfolio> GetPortfolio()
        {       
            var repos = await _gitHubClient.Repository.GetAllForCurrent();
            var reposytory = new List<RepositoryDto>();
            foreach (var repo in repos)
            {
                var pullRequests = await _gitHubClient.PullRequest.GetAllForRepository(repo.Owner.Login, repo.Name);
                var languagesDict = await _gitHubClient.Repository.GetAllLanguages(repo.Owner.Login, repo.Name);
                var languages = languagesDict.Select(la => la.Name).ToList();
                reposytory.Add(new RepositoryDto
                {
                    RepositoryName = repo.Name,
                    Languages = languages,
                    LastCommitDate = repo.UpdatedAt.UtcDateTime,
                    Stars = repo.StargazersCount,
                    PullRequestsCount = pullRequests.Count,
                    RepoUrl = repo.HtmlUrl
                });
            }
            var p = new Portfolio();
            p.UserName = _options.UserName;
            p.Repositories = reposytory;
            return p;

        }
        public async Task<List<RepositoryDto>> SearchRepositories(string repoName = "", string language = "", string username = "")
        {
            var query = " ";
            if (!string.IsNullOrEmpty(repoName))
                query += $"{repoName} in:name ";
            if (!string.IsNullOrEmpty(language))
                query += $"language:{language} ";
            if (!string.IsNullOrEmpty(username))
                query += $"user:{username} ";
            var request = new SearchRepositoriesRequest(query);
            var result = await _gitHubClient.Search.SearchRepo(request);

            var portfolio = new List<RepositoryDto>();
            foreach (var repo in result.Items)
            {
                var pullRequests = await _gitHubClient.PullRequest.GetAllForRepository(repo.Owner.Login, repo.Name);
                var languagesDict = await _gitHubClient.Repository.GetAllLanguages(repo.Owner.Login, repo.Name);
                var languages = languagesDict.Select(la => la.Name).ToList();
                portfolio.Add(new RepositoryDto
                {
                    RepositoryName = repo.Name,
                    Languages = languages,
                    LastCommitDate = repo.UpdatedAt.UtcDateTime,
                    Stars = repo.StargazersCount,
                    PullRequestsCount = pullRequests.Count,
                    RepoUrl = repo.HtmlUrl
                });
            }
            return portfolio;
        }
    }

}
