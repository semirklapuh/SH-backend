namespace SofthouseTaskApis.Models.User;

public class DemoUser
{
    public string Username { get; set; }
    public int Pin { get; set; }

    public List<DemoUser> GetDemoUsers()
    {
        var demoUsers = new List<DemoUser>
        {
            new()
            {
                Username = "SofthouseUser1",
                Pin = 1234
            },
            new()
            {
                Username = "SofthouseUser2",
                Pin = 4321
            },
            new()
            {
                Username = "SofthouseUser3",
                Pin = 0000
            }
        };

        return demoUsers;
    }
}