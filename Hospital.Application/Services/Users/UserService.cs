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
      var jwtKey = _configuration["Keys:JWTKey"]!;
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.NameId, "samc9201@hotmail.com"),
        new Claim(JwtRegisteredClaimNames.FamilyName, "admin"),
        new Claim(JwtRegisteredClaimNames.GivenName, "samc9201@hotmail.com"),
        new Claim(JwtRegisteredClaimNames.UniqueName, "1"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString(), ClaimValueTypes.Integer64),
      };

      var token =
        new JwtSecurityToken(
          issuer: "www.saul.com.mx",
          audience: "www.saul.com.mx",
          claims: claims,
          expires: DateTime.UtcNow.AddHours(8),
          notBefore: DateTime.UtcNow,
          signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
