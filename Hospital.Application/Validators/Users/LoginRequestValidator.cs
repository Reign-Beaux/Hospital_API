using Hospital.Application.DTOs.Users.Request;
using Hospital.Application.Models;

namespace Hospital.Application.Validators.Users
{
  public static class LoginRequestValidator
  {
    public static ValidationResult ValidateRequest(UserLoginRequest request)
    {
      ValidationResult validationResult = new ValidationResult();

      if (string.IsNullOrEmpty(request.UserName))
        validationResult.Errors.Add("El campo usuario es requerido");
      if (string.IsNullOrEmpty(request.Password))
        validationResult.Errors.Add("El campo password es requerido");

      validationResult.IsValid = validationResult.Errors.Count() == 0;

      return validationResult;
    }
  }
}
