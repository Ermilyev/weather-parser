using Common.Infrastructure.Utility.EF;
using Common.Infrastructure.Utility.Serilog;
using Common.Infrastructure.Utility.Swagger;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebApi.Services;

/// <summary>
/// 
/// </summary>
public class Startup
{
    private IConfiguration Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration) 
        => Configuration = configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddMvc(options => options.Filters
        .Add(new TrackPerformanceFilter("Parser", "WebApi")));
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
            }).AddControllersAsServices();
        services.AddAutoMapper(typeof(Startup));
        //services.AddLogger();
        services.Configure<EFConfig>(Configuration.GetSection("EFConfig"));
        services.AddRepositories()
            .AddServices()
            .AddMapperProfiles()
            .AddInfrastructure();
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        services.AddHealthChecks();
        services.AddSwagger(true);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="environment"></param>
    /// <param name="provider"></param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IApiVersionDescriptionProvider provider)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseMiddleware<ExceptionHandler>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger(provider);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });
    }
}