using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.ExtensionProperties;

public class AddExtensionPropertyFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var extensionProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<OpenApiExtensionPropertyAttribute>() != null);

        foreach (var extensionProperty in extensionProperties)
        {
            var extensionPropertyAttribute =
                (OpenApiExtensionPropertyAttribute) Attribute.GetCustomAttribute(extensionProperty, typeof (OpenApiExtensionPropertyAttribute))!;
            
            var prop = schema.Properties.Keys.SingleOrDefault(x =>
                string.Equals(x, extensionProperty.Name, StringComparison.OrdinalIgnoreCase));

            if (prop != null)
                schema.Properties[prop].Extensions.Add(
                    extensionPropertyAttribute.ExtensionPropertyName, new OpenApiString(extensionPropertyAttribute.ExtensionPropertyValue));
        }
    }
}