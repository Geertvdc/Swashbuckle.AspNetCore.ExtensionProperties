namespace Swashbuckle.AspNetCore.ExtensionProperties;

[AttributeUsage(AttributeTargets.Property)]
public class FakerAttribute : OpenApiExtensionPropertyAttribute
{

    public FakerAttribute(string fakerValue) : base("x-faker", fakerValue)
    {
    }
}