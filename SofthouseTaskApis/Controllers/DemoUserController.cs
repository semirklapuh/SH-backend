using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SofthouseTaskApis.Handlers.Interfaces.DemoUser;
using SofthouseTaskApis.Models.User;

namespace SofthouseTaskApis.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class DemoUserController : Controller
{
    private readonly IDemoUserService _demoUserService;

    public DemoUserController(IDemoUserService demoUserService)
    {
        _demoUserService = demoUserService;
    }

    [HttpPost("authenticate-user")]
    [AllowAnonymous]
    public async Task<ActionResult> AuthenticateUser([FromBody] DemoUser user)
    {
        try
        {
            var token = await _demoUserService.AuthenticateUser(user);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}