using System.Text.Json.Serialization;
using Asp.Versioning.ApiExplorer;
using Common.Infrastructure.Utilities.Swagger;
using Weather.Api;
using Weather.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services
    .AddRouting(options => options.LowercaseUrls = false)
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwagger();
builder.Services.AddApiVersioning().AddApiExplorer(opt => { opt.SubstituteApiVersionInUrl = true; });
builder.Services.AddHealthChecks();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepository();
builder.Services.AddServices();


var app = builder.Build();
app.UseRouting();
app.UseCors(x =>
{
    x.AllowCredentials()
        .SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger(provider);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MyDbContext>();
    context.Database.EnsureCreated();
}

app.Run();