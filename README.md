# Swashbuckle.AspNetCore.ExtensionProperties

[![Build, Test & Publish nuget package](https://github.com/Geertvdc/Swashbuckle.AspNetCore.ExtensionProperties/actions/workflows/build-nugetpackage.yml/badge.svg)](https://github.com/Geertvdc/Swashbuckle.AspNetCore.ExtensionProperties/actions/workflows/build-nugetpackage.yml)
[![Nuget](https://img.shields.io/nuget/v/Swashbuckle.AspNetCore.ExtensionProperties)](https://www.nuget.org/packages/Swashbuckle.AspNetCore.ExtensionProperties/)
[![Maintainability](https://api.codeclimate.com/v1/badges/dd225537f49dbefba874/maintainability)](https://codeclimate.com/github/Geertvdc/Swashbuckle.AspNetCore.ExtensionProperties/maintainability)
[![Test Coverage](https://api.codeclimate.com/v1/badges/dd225537f49dbefba874/test_coverage)](https://codeclimate.com/github/Geertvdc/Swashbuckle.AspNetCore.ExtensionProperties/test_coverage)

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


## Real life use case: Generating sandbox APIs from OpenAPI Spec files

Often when building APIs you want to be able to create a sandbox sample for your consumers to test your APIs. This can be done by tools like [Open API Mocker](https://github.com/jormaechea/open-api-mocker) that take an OpenAPI spec and generates a sample API based on that spec.
A feature of Open API Mocker is that it can generate fake data using a library called [Faker](https://github.com/faker-js/faker) by adding extension properties with the name `x-faker` to the OpenAPI spec.

In the samples folder you can find a sample API that implements this feature.
So how does it work?

Create a new class that is used as a contract for the API and add the `Faker` attribute to it (which inherits from the `OpenApiExtensionProperty` attribute). The Faker library has a wide variaty of options to generate fake data that you can find on the documentation page of the [Faker library](https://github.com/faker-js/faker).

```C#
public class WeatherForecast
{
    [Faker(fakerValue:"date.recent")]
    public DateTime Date { get; set; }

    [Faker(fakerValue:"datatype.number(-10,35)")]
    public int TemperatureC { get; set; }

    [Faker(fakerValue:"datatype.number(105)")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [Faker(fakerValue:"lorem.paragraph")]
    public string? Summary { get; set; }
}
```

When we run the API you can see that the `x-faker` properties are added to the OpenAPI spec.

```json
"schemas": {
  "WeatherForecast": {
    "type": "object",
    "properties": {
      "date": {
        "type": "string",
        "format": "date-time",
        "x-faker": "date.recent"
      },
      "temperatureC": {
        "type": "integer",
        "format": "int32",
        "x-faker": "datatype.number(-10,35)"
      },
      "temperatureF": {
        "type": "integer",
        "format": "int32",
        "readOnly": true,
        "x-faker": "datatype.number(105)"
      },
      "summary": {
        "type": "string",
        "nullable": true,
        "x-faker": "lorem.paragraph"
      }
    },
    "additionalProperties": false
  }
}
```

Next steps are saving the OpenAPI spec to a file and passing it to the Open API Mocker.
Downloading the OpenAPI spec can be done through the `Swashbuckle.AspNetCore.Cli` tool

```Bash
dotnet tool install -g --version 6.2.3 Swashbuckle.AspNetCore.Cli

swagger tofile --output swagger.json sample/SampleApiWithFakerStub/bin/Debug/net6.0/SampleApiWithFakerStub.dll "v1" 
```

Now we can run the Mock API using docker

```Bash
docker run -v "[path to your]swagger.json:/app/schema.json" -p "8080:5000" jormaechea/open-api-mocker
```

You can now do calls to localhost:8080 to test the API and you should get back fake data like this:

```json
[
  {
    "date":"2022-01-05T22:25:30.366Z",
    "temperatureC":-6,
    "temperatureF":41,
    "summary":"Praesentium iste natus temporibus omnis nihil perspiciatis quo. Rerum odit blanditiis quia autem et earum magnam quod. Suscipit voluptate quia voluptatibus ea reiciendis. Sed praesentium sed in est."
  }
]
```
