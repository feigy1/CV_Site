using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubService.Entites
{
    public class Portfolio
    {
        public String UserName { get; set; }
        public List<RepositoryDto> Repositories { get; set; }
    }
}
