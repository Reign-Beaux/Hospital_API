using Hospital.Application.DTOs.Users.Request;
using Hospital.Application.Models;

namespace Hospital.Application.Services.Users
{
  public interface IUserService
  {
    Task<Response<string>> Login(UserLoginRequest request);
  }
}
