using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Utilities.Swagger;

public class ExtraSwaggerOptions(
    IApiVersionDescriptionProvider? apiVersionDescriptionProvider)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
        {
            AddVersionSwaggerDocs(options);
            options.DocInclusionPredicate((docName, description) =>
            {
                if (description.GroupName != docName || !description.TryGetMethodInfo(out var methodInfo))
                {
                     return false;
                }
                
                var versions = methodInfo.DeclaringType?.GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .Union(methodInfo.GetCustomAttributes(true).OfType<ApiVersionAttribute>())
                    .SelectMany(x => x.Versions)
                    .Distinct();

                return versions != null && versions.Any(v => v.ToString() == docName);
            });

            options.SchemaFilter<RecordSchemeFilter>();
        }

        private void AddVersionSwaggerDocs(SwaggerGenOptions options)
        {
            foreach(var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    $"{description.ApiVersion}",
                    new OpenApiInfo
                    {
                        Title = "Weather API",
                        Version = $"v{description.ApiVersion}",
                        Description = "The best Api",
                        TermsOfService = new Uri("http://test.org/"),
                        Contact = new OpenApiContact
                        {
                            Name = "User",
                            Email = "user@user.com"
                        }
                    }
                );
            }
        }
    }
