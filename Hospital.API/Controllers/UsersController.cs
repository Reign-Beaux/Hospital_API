using Hospital.Application.DTOs.Users.Request;
using Hospital.Application.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
      _service = service;
    }


    [AllowAnonymous]
    [HttpPost("Generate/Token")]
    public async Task<IActionResult> GenerateToken([FromBody] UserLoginRequest request)
    {
      var response = await _service.Login(request);
      return Ok(response);
    }
  }
}
