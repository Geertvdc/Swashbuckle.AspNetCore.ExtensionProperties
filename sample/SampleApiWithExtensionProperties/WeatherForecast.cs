using Swashbuckle.AspNetCore.ExtensionProperties;

namespace SampleApiWithExtensionProperties;

public class WeatherForecast
{
    [OpenApiExtensionProperty(extensionPropertyName: "x-date", extensionPropertyValue:"somevalue")]
    public DateTime Date { get; set; }

    [OpenApiExtensionProperty(extensionPropertyName: "x-someproperty", extensionPropertyValue:"value2")]
    public int TemperatureC { get; set; }

    [OpenApiExtensionProperty(extensionPropertyName: "x-temperatureFarhenheit", extensionPropertyValue:"value3")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [OpenApiExtensionProperty(extensionPropertyName: "x-backendsystem", extensionPropertyValue:"value4")]
    public string? Summary { get; set; }
}