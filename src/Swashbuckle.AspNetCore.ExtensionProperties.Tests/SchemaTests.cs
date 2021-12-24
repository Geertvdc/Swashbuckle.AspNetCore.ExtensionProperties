
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace Swashbuckle.AspNetCore.ExtensionProperties.Tests;

public class SchemaTests
{
    
    [Fact]
    public void TestSwagger()
    {
        var schema = new OpenApiSchema()
        {
            Properties = new Dictionary<string, OpenApiSchema>()
            {
                {
                    "id", new OpenApiSchema() { Type = "integer", Format = "int32" }
                },
                {
                    "name", new OpenApiSchema() { Type = "string" }
                }
            }
        };

        var inputContext = new SchemaFilterContext(typeof(Person),null,null);
        var filter = new AddExtensionPropertyFilter();
        filter.Apply(schema, inputContext);

        var idProperty = schema.Properties["id"];
        OpenApiString? fakerExtensionProperty = idProperty.Extensions["x-faker"] as OpenApiString;
        Assert.NotNull(fakerExtensionProperty);
        Assert.Equal("datatype.number",fakerExtensionProperty?.Value);
        
        var nameProperty = schema.Properties["name"];
        OpenApiString? nameExtensionProperty = nameProperty.Extensions["x-name"] as OpenApiString;
        Assert.NotNull(nameExtensionProperty);
        Assert.Equal("Geert",nameExtensionProperty?.Value);
    }
}