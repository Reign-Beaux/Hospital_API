using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Hospital.API.Extensions
{
  public static class SwaggerExtensions
  {
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
      var openApi = new OpenApiInfo
      {
        Title = "Hospital API",
        Version = "Alfa",
        Description = "Hospital API 2023",
        TermsOfService = new Uri("https://opensource.org/licenses/NIT"),
        Contact = new OpenApiContact
        {
          Name = "Saúl Antonio",
          Email = "samc920130@gmail.com",
          Url = new Uri("https://www.facebook.com/xSiegfried"),
        },
        License = new OpenApiLicense
        {
          Name = "Use under LICX",
          Url = new Uri("https://opensource.org/licenses/NIT")
        }
      };

      services.AddSwaggerGen(x =>
      {
        openApi.Version = "v1";
        x.SwaggerDoc("v1", openApi);

        var securityScheme = new OpenApiSecurityScheme
        {
          Name = "JWT Authentication",
          Description = "JWT Bearer Token",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http,
          Scheme = "Bearer",
          BearerFormat = "JWT",
          Reference = new OpenApiReference
          {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
          }
        };

        x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
        x.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          { securityScheme, new string[] { } }
        });
      });

      return services;
    }
  }
}
