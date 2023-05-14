using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using SofthouseTaskApis.Handlers.Interfaces.Github;
using SofthouseTaskApis.Models.Github;

namespace SofthouseTaskApis.Handlers.Services.Github;

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;

    public GithubService()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
        {
            return true;
        };
        _httpClient = new HttpClient(clientHandler);
    }

    public async Task<List<GithubRepository>> GetGithubUserRepositories(string username)
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SofthouseTask", "1.0"));

        var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/repos");

        if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception(response.ReasonPhrase);

        var repositories = await response.Content.ReadFromJsonAsync<List<GithubRepository>>();
        return repositories;
    }

    public Task<GithubRepository> UpdateGithubUserRepository(GithubRepository updatedRepository, [Required] string username)
    {
        SaveObjectToFile(updatedRepository, username);
        return Task.FromResult(updatedRepository);
    }

    private Task SaveObjectToFile(GithubRepository repository, string username)
    {
        var json = JsonSerializer.Serialize(repository);
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "UpdatedFiles");
        
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);
        
        File.WriteAllText(filePath + $"/{username + repository.Name}.json", json);
        return Task.CompletedTask;
    }
}