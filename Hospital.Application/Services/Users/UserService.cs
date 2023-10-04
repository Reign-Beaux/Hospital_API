using Hospital.Application.DTOs.Users.Request;
using Hospital.Application.Models;
using Hospital.Application.Services._GenericService;
using Hospital.Application.Validators.Users;
using Hospital.Infraestructure.Persistences.UnitsOfWork;
using Hospital.Utilities.Statics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Hospital.Application.Services.Users
{
  public class UserService : GenericService, IUserService
  {
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration, IUnitOfWork unitOfWork) : base(unitOfWork)
    {
      _configuration = configuration;
    }

    public async Task<Response<string>> Login(UserLoginRequest request)
    {
      Response<string> response = new();
      var validationResult = LoginRequestValidator.ValidateRequest(request);
      if (!validationResult.IsValid)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.VALIDATE;
        response.Errors = validationResult.Errors;

        return response;
      };

      var user = await _unitOfWork.UserRepository.GetByUsername(request.UserName);

      if (user == null)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.LOGIN_USER_EMPTY;
      }
      else if (request.Password != user.Password)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.LOGIN_PASSWORD_BAD;
      }
      else
      {
        response.IsSuccess = true;
        response.Message = ReplyMessage.LOGIN_SUCCESS;
        response.Data = GenerateToken();
      }

      return response;
    }

    private string GenerateToken()
    {

      var claims = new List<Claim>
      {
        new(ClaimTypes.Name, "admin-temp"),
        new(ClaimKeys.USER, JsonSerializer.Serialize(
          new {
            Username = "-admin-",
            FullName = "Saúl Antonio Morquecho Cela",
            Email = "samc920130@hotmail.com",
            RoleDescription = "Admin"
          }
        )),
      };
      var jwtKey = _configuration["Keys:JWTKey"]!;

      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
      var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var expiracion = DateTime.UtcNow.AddDays(1);

      var token =
        new JwtSecurityToken(
          issuer: null,
          audience: null,
          claims: claims,
          expires: expiracion,
          signingCredentials: signingCredentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
