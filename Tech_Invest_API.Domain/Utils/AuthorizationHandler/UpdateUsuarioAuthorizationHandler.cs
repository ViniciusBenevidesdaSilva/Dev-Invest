using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Tech_Invest_API.Domain.Utils.AuthorizationHandler;

public class UpdateUsuarioAuthorizationHandler : AuthorizationHandler<UpdateUsuarioRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UpdateUsuarioAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdateUsuarioRequirement requirement)
    {
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is not null)
        {
            var routeData = _httpContextAccessor.HttpContext.GetRouteData()!;
            routeData.Values.TryGetValue("id", out var routeId);

            if (routeId.ToString() == userIdClaim.Value)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

