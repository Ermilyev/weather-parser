using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Common.Infrastructure.Utility.Swagger
{
    public class RecordTypeSchemeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            bool isRecord =
                    context.MemberInfo?.DeclaringType.GetMethods().Any(m => m.Name == "<Clone>$") ?? false;

            if (!isRecord)
                return;

            ConstructorInfo[] constructors = context.MemberInfo?.DeclaringType.GetConstructors() ?? Array.Empty<ConstructorInfo>();

            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] targets = constructor.GetParameters()
                    .Where(item => item.GetCustomAttributes(true).OfType<RequiredAttribute>().Any())
                    .ToArray();

                string[] names = targets.Select(item => item.Name).ToArray();
                if (names.Contains(context.MemberInfo.Name))
                    schema.Nullable = false;
            }
        }
    }
}