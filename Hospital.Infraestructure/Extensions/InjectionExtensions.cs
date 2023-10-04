using Hospital.Infraestructure.Persistences.UnitsOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infraestructure.Extensions
{
  public static class InjectionExtensions
  {
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      return services;
    }
  }
}
