using SofthouseTaskApis.Models.Github;

namespace SofthouseTaskApis.Handlers.Interfaces.Github;

public interface IGithubService
{
    Task<List<GithubRepository>> GetGithubUserRepositories(string username);

    Task<GithubRepository> UpdateGithubUserRepository(GithubRepository updatedRepository, string username);
}