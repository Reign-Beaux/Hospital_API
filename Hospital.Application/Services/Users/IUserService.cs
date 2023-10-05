using Hospital.Application.DTOs.Users.Request;
using Hospital.Application.Models;

namespace Hospital.Application.Services.Users
{
  public interface IUserService
  {
    /// <summary>
    /// Validate credentials and create the token.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Response<string>> Login(UserLoginRequest request);
  }
}
