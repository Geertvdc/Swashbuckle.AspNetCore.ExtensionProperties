# Swashbuckle.AspNetCore.ExtensionProperties

[![Build, Test & Publish nuget package](https://github.com/Geertvdc/Swashbuckle.AspNetCore.ExtensionProperties/actions/workflows/build-nugetpackage.yml/badge.svg)](https://github.com/Geertvdc/Swashbuckle.AspNetCore.ExtensionProperties/actions/workflows/build-nugetpackage.yml)
[![Nuget](https://img.shields.io/nuget/v/Swashbuckle.AspNetCore.ExtensionProperties)](https://www.nuget.org/packages/Swashbuckle.AspNetCore.ExtensionProperties/)

Swashbuckle.AspNetCore.ExtensionProperties is a nuget package that adds extension properties for swashbuckle to add custom `"x-#####"` properties in the OpenAPI spec file for your .Net web API. This can be used to add properties needed by specific backend systems or to add fake stub data for example.

Documentation about Extension Properties / Vendor Extensions in OpenAPI Spec:
- https://spec.openapis.org/oas/v3.1.0#specification-extensions
- https://swagger.io/docs/specification/2-0/swagger-extensions/
- https://github.com/Mermade/openapi-specification-extensions

## Usage:

Add an `OpenApiExtensionProperty` attribute to your properties that are used in the OpenAPI spec.
```C#
[OpenApiExtensionProperty(extensionPropertyName: "x-date", extensionPropertyValue:"somevalue")]
public DateTime Date { get; set; }
```

Add the `AddExtensionPropertyFilter` filter to the swagger generation
```C#
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<AddExtensionPropertyFilter>();
});
```

This will result in a property with the `extensionPropertyName` and `extensionPropertyValue` in the generated OpenAPI Spec file in either yaml or json

#### yaml:
```yaml
      properties:
        date:
          type: string
          format: date-time
          x-date: somevalue
```
#### json:
```json
        "properties": {
          "date": {
            "type": "string",
            "format": "date-time",
            "x-date": "somevalue"
          },
```
