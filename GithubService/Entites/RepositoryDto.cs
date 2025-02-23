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
        // רשימת שפות הפיתוח
        public List<string> Languages { get; set; }
        // תאריך הקומיט האחרון (אם נמצא)
        public DateTime? LastCommitDate { get; set; }
        // מספר הכוכבים
        public int Stars { get; set; }
        // מספר ה-Pull Requests (פתוחים וסגורים)
        public int PullRequestsCount { get; set; }
        // קישור לאתר/דף הבית של המאגר, במידה וקיים
        public string RepoUrl { get; set; }
    }
}
