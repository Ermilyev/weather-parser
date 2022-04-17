using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Utility.Swagger
{
    public static class SwaggerRegistration
    {
        public static IServiceCollection AddSwaggerNewton(this IServiceCollection services, bool includeXmlComments = false, Action<SwaggerGenOptions> setupAction = null)
        {
            return services
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerGen()
                .AddSingleton<IConfigureOptions<SwaggerGenOptions>>(x =>
                    new ExtraSwaggerOptions(x.GetService<IApiVersionDescriptionProvider>()));
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, bool includeXmlComments = false, Action<SwaggerGenOptions> setupAction = null)
        {
            return services
                .AddSwaggerGen(c =>
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()))
                .AddSingleton<IConfigureOptions<SwaggerGenOptions>>(x =>
                    new ExtraSwaggerOptions(x.GetService<IApiVersionDescriptionProvider>()));
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }
}
