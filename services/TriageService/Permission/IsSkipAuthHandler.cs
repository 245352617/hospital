using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

public class IsSkipAuthRequirement : IAuthorizationRequirement
{
}

public class IsSkipAuthHandler : AuthorizationHandler<IsSkipAuthRequirement>
{
    private readonly IConfiguration _configuration;

    public IsSkipAuthHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsSkipAuthRequirement requirement)
    {
        // 获取配置中的值
        bool isSkipAuth = _configuration.GetValue<bool>("IsSkipAuth");

        if (isSkipAuth)
        {
            context.Succeed(requirement);
        }

        // 其他授权逻辑...

        return Task.CompletedTask;
    }
}