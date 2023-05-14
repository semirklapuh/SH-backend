using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SofthouseTaskApis.Handlers.Interfaces.Github;
using SofthouseTaskApis.Models.Github;

namespace SofthouseTaskApis.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public class GithubController : Controller
{
    private readonly IGithubService _githubService;

    public GithubController(IGithubService githubService)
    {
        _githubService = githubService;
    }

    [HttpGet("get-user-repositories")]
    public async Task<ActionResult> GetUserRepositories(string username)
    {
        try
        {
            return Ok(await _githubService.GetGithubUserRepositories(username));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("update-user-repository")]
    public async Task<ActionResult> UpdateUserRepository([FromBody] GithubRepository repository, string username)
    {
        return Ok(await _githubService.UpdateGithubUserRepository(repository, username));
    }
}