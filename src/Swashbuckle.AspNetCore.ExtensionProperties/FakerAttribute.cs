namespace Swashbuckle.AspNetCore.ExtensionProperties;

[AttributeUsage(AttributeTargets.Property)]
public class FakerAttribute : OpenApiExtensionPropertyAttribute
{

    public FakerAttribute(string extensionPropertyValue) : base("x-faker", extensionPropertyValue)
    {
    }
}