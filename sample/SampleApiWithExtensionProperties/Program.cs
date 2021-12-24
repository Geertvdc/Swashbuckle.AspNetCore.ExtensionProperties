using Swashbuckle.AspNetCore.ExtensionProperties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<AddExtensionPropertyFilter>();
});
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => { x.SwaggerEndpoint("v1/swagger.yaml", "Sample API with extension properties"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();