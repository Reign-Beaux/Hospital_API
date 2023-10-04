using Hospital.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Application.Extensions
{
  public static class InjectionEntensions
  {
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
