using Asp.Versioning;
using FSH.Framework.Persistence;
using FSH.Framework.Web.Modules;
using FSH.Modules.StudentManagement.Features.v1.Eleves.CreateEleve;

using FSH.Modules.StudentManagement.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace FSH.Modules.StudentManagement;

public class StudentManagementModule : IModule
{
    public void ConfigureServices(IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddHeroDbContext<StudentDbContext>();  
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<StudentDbContext>(              
                name: "db:student",
                failureStatus: HealthStatus.Unhealthy);
    }

   
 public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var apiVersionSet = endpoints.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();


        var group = endpoints
            .MapGroup("api/v{version:apiVersion}/student")
            .WithTags("Student")
            .WithApiVersionSet(apiVersionSet);

       
        group.MapCreateEleveEndpoint();
       
    }
}

