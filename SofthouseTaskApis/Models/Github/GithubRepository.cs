namespace SofthouseTaskApis.Models.Github;

public class GithubRepository
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Html_url { get; set; }
    public string Clone_url { get; set; }
    public string Ssh_url { get; set; }
    public string Visibility { get; set; }
}