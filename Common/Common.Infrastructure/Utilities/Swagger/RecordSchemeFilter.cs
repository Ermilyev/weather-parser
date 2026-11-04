using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Utilities.Swagger;

public sealed class RecordSchemeFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        var isRecord = context.MemberInfo?.DeclaringType?.GetMethods().Any(m => m.Name == "<Clone>$") ?? false;

        if (isRecord == false)
        { 
            return;
        }
        
        var constructors = context.MemberInfo?.DeclaringType?.GetConstructors() ?? [];

        foreach(var constructor in constructors)
        {
            var targets = constructor.GetParameters()
                .Where(item => item.GetCustomAttributes(true).OfType<RequiredAttribute>().Any())
                .ToArray();

            var names = targets.Select(item => item.Name).ToArray();
            if(names.Contains(context.MemberInfo?.Name))
                if (schema is OpenApiSchema concreteSchema && concreteSchema.Type is { } type)
                    concreteSchema.Type = type & ~JsonSchemaType.Null;
        }
    }
}
