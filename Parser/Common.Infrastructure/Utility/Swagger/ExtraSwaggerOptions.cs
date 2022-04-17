using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Utility.Swagger
{
    public class ExtraSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ExtraSwaggerOptions(IApiVersionDescriptionProvider provider)
           => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            var xmlPathServices = $@"{AppDomain.CurrentDomain.BaseDirectory}\WebApi.Services.XML";
            var xmlPathModels = $@"{AppDomain.CurrentDomain.BaseDirectory}\WebApi.Models.XML";
            foreach (var description in _provider.ApiVersionDescriptions)
            {

                options.DescribeAllParametersInCamelCase();
                options.IncludeXmlComments(xmlPathServices);
                options.IncludeXmlComments(xmlPathModels);
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }

            options.SchemaFilter<RecordTypeSchemeFilter>();
            options.SchemaFilter<EnumTypesSchemaFilter>(xmlPathServices);
            options.DocumentFilter<EnumTypesDocumentFilter>();
        }

        public void Configure(string name, SwaggerGenOptions options)
       => Configure(options);

        private static OpenApiInfo CreateVersionInfo(
            ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Weather API",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
