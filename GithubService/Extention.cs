using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubService
{
    public static class Extention
    {
        public static void AddGitHubService(this IServiceCollection services, Action<GitHubSetting> configurationOption)
        {
            services.Configure(configurationOption);
            services.AddScoped<IGitHubService, GitHubService1>();
        }
    }
}
