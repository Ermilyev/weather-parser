using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Utilities.Swagger;
  
  public static class SwaggerExtensions
  {
      public static IServiceCollection AddSwagger(this IServiceCollection services)
      {
          return services
              .AddSwaggerGen(options =>
              {
                  options.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());
                  options.EnableAnnotations();
                  
                  const string xmlFile = "Weather.Api.xml";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                  
                  options.IncludeXmlComments(xmlPath);
              })
              .AddSingleton<IConfigureOptions<SwaggerGenOptions>>(x =>
                  new ExtraSwaggerOptions(x.GetService<IApiVersionDescriptionProvider>()));
      }

      public static IApplicationBuilder UseSwagger(this IApplicationBuilder app,
          IApiVersionDescriptionProvider? provider)
      {
          app.UseSwagger();

          if (provider != null)
          {
              app.UseSwaggerUI(options =>
              {
                  foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                  {
                      options.SwaggerEndpoint($"{description.ApiVersion}/swagger.json",
                          $"v{description.ApiVersion}".ToUpperInvariant());
                  }
              });
          }
          else
          {
              app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "My API V1"));
          }

          return app;
      }
  }