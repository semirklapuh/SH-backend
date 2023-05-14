using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SofthouseTaskApis.Handlers.Interfaces.DemoUser;

namespace SofthouseTaskApis.Handlers.Services.DemoUser;

public class DemoUserService : IDemoUserService
{
    private readonly IConfiguration _configuration;

    public DemoUserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> AuthenticateUser(Models.User.DemoUser user)
    {
        var demoUsers = new Models.User.DemoUser().GetDemoUsers();
        if (!demoUsers.Any(x => x.Username == user.Username && x.Pin == user.Pin))
            throw new Exception("Specific user not found.");
        
        return CreateToken(user);
    }

    private string CreateToken(Models.User.DemoUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );
        
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}