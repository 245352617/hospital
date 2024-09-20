using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.ECIS.Core.Middlewares;

public class ApiResponseHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ApiResponseHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var a = AuthenticateResult.Success(null);
        return Task.FromResult<AuthenticateResult>(a);
        //throw new NotImplementedException();
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.ContentType = "application/json";
        Response.StatusCode = StatusCodes.Status401Unauthorized;

        var ret = new ResponseBase<string>(EStatusCode.C401, "未能获取到登录身份信息，请检查后重试 (×_×)");
        var errmsg = JsonConvert.SerializeObject(ret);
        await Response.WriteAsync(errmsg);
    }


    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.ContentType = "application/json";
        Response.StatusCode = StatusCodes.Status403Forbidden;
        var errmsg = JsonConvert.SerializeObject(new ResponseBase<string>(EStatusCode.C403, "您没有当前操作或访问权限"));
        await Response.WriteAsync(errmsg);
    }
}