using FSH.Framework.Shared.Identity.Authorization;
using FSH.Modules.StudentManagement.Contracts.v1.Eleves.CreateEleve;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Modules.StudentManagement.Features.v1.Eleves.CreateEleve;

public static class CreateEleveEndpoint
{

    public static RouteHandlerBuilder MapCreateEleveEndpoint(
       this IEndpointRouteBuilder endpoints)
    {
        
        return endpoints.MapPost("/eleves", async (
            IMediator mediator,
            [FromBody] CreateEleveCommand request,
            CancellationToken cancellationToken) =>
        {
            
            var id = await mediator.Send(request, cancellationToken);

          
            return TypedResults.Created($"/api/v1/student/eleves/{id}", id);
        })
      
        .WithName("CreateEleve")                    
        .WithSummary("Create a new student")         

        
        .RequirePermission(StudentManagementPermissionConstants.Eleves.Create)

        .Produces<Guid>(StatusCodes.Status201Created)       
        .Produces(StatusCodes.Status400BadRequest)         
        .Produces(StatusCodes.Status401Unauthorized)       
        .Produces(StatusCodes.Status403Forbidden);          
    }
}
