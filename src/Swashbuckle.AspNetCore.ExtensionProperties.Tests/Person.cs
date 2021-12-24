namespace Swashbuckle.AspNetCore.ExtensionProperties.Tests;

public class Person
{
    [Faker(fakerValue: "datatype.number")]
    public int Id { get; set; }
    
    [OpenApiExtensionProperty(extensionPropertyName:"x-name", extensionPropertyValue:"Geert")]
    public string? Name { get; set; }
}