using GithubService.Entites;
using GithubService;
using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace WebApi.CachedService
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;
        private const string UserPortfolioKey = "UserPortfolio";

        public CachedGitHubService(IGitHubService gitHubService, IMemoryCache memoryCache)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
        }

        public async Task<Portfolio> GetPortfolio()
        {
            if (_memoryCache.TryGetValue(UserPortfolioKey, out Portfolio portfolio))
            {
                var lastUpdated = portfolio.Repositories.Max(r => r.LastCommitDate);
                var repos = await _gitHubService.GetPortfolio();

                bool isUpdated = repos.Repositories.Any(r => r.LastCommitDate > lastUpdated);

                if (!isUpdated)
                {
                    Console.WriteLine("Returning data from cache.");
                    return portfolio;
                }

                Console.WriteLine("Cache invalidated - fetching new data.");
            }

            portfolio = await _gitHubService.GetPortfolio();
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
            _memoryCache.Set(UserPortfolioKey, portfolio, cacheOptions);

            return portfolio;
        }

        public async Task<int> GetUserFollowersAsync()
        {
            return await _gitHubService.GetUserFollowersAsync();
        }

        public async Task<List<Repository>> SearchRepositoriesInCSharp()
        {
            return await _gitHubService.SearchRepositoriesInCSharp();
        }

        public async Task<List<RepositoryDto>> SearchRepositories(string repoName = "", string language = "", string username = "")
        {
            return await _gitHubService.SearchRepositories(repoName, language, username);
        }
    }
}
