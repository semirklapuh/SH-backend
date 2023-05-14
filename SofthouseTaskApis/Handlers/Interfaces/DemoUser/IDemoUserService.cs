namespace SofthouseTaskApis.Handlers.Interfaces.DemoUser;

public interface IDemoUserService
{
    Task<string> AuthenticateUser(Models.User.DemoUser user);
}