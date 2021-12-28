using Swashbuckle.AspNetCore.ExtensionProperties;

namespace SampleApiWithFakerStub;

public class WeatherForecast
{
    [Faker(fakerValue:"date.recent")]
    public DateTime Date { get; set; }

    [Faker(fakerValue:"random.number")]
    public int TemperatureC { get; set; }

    [Faker(fakerValue:"random.number")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [Faker(fakerValue:"lorem.paragraph")]
    public string? Summary { get; set; }
}