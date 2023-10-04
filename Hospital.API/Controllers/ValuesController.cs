using Hospital.Application.DTOs.Users.Request;
using Hospital.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private readonly IUserService _service;

    public ValuesController(IUserService service)
    {
      _service = service;
    }


    [HttpPost("Generate/Token")]
    public async Task<IActionResult> GenerateToken([FromBody] UserLoginRequest request)
    {
      var response = await _service.Login(request); //_application.GenerateToken(requestDTO);
      return Ok(response);
    }
  }
}
