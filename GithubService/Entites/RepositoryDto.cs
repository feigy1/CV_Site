using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubService.Entites
{
    public class RepositoryDto
    {
        public string RepositoryName { get; set; }
        public List<string> Languages { get; set; }
        public DateTime? LastCommitDate { get; set; }
        public int Stars { get; set; }
        public int PullRequestsCount { get; set; }
        public string RepoUrl { get; set; }
    }
}
