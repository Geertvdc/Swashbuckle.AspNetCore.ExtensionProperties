namespace Swashbuckle.AspNetCore.ExtensionProperties;

[AttributeUsage(AttributeTargets.Property)]
public class OpenApiExtensionPropertyAttribute : Attribute
{
    public string ExtensionPropertyName { get; private set; }
    public string ExtensionPropertyValue { get; private set; }
    
    public OpenApiExtensionPropertyAttribute(string extensionPropertyName, string extensionPropertyValue)
    {
        if (!extensionPropertyName.StartsWith("x-"))
        {
            throw new ArgumentException("Extension property name must start with 'x-' to conform to OpenAPI extension property naming convention.");
        }

        ExtensionPropertyName = extensionPropertyName;
        ExtensionPropertyValue = extensionPropertyValue;
    }
}