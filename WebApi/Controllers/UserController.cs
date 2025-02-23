using GithubService.Entites;
using GithubService;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using GithubService.Entites;
using GithubService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        public UserController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }
        // GET: api/<UserController>
        [HttpGet("/followers/")]
        public async Task<ActionResult<int>> Get1()
        {
            return await _gitHubService.GetUserFollowersAsync();
        }

        //GET api/<UserController>/5
        [HttpGet("/User/")]
        public async Task<List<RepositoryDto>> Get2(

            [FromQuery] string repoName = "", [FromQuery] string language = "", [FromQuery] string username = "")
        {
            return await _gitHubService.SearchRepositories(repoName, language, username);
        }
        [HttpGet("/cSharp/")]
        public async Task<List<Repository>> Get3()
        {
            return await _gitHubService.SearchRepositoriesInCSharp();
        }
        [HttpGet("/myPortfolio")]
        public async Task<Portfolio> Get4()
        {
            return await _gitHubService.GetPortfolio();
        }
    }
}
