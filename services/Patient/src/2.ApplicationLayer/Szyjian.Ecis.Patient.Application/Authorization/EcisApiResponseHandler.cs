using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Volo.Abp.Security.Claims;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{

    /// <summary>
    /// 自定义鉴权，用于跳过认证中心校验
    /// 注：因无法取得认证中心 jwt 的 key，所以无法对 token 合法性进行校验，对应伪造的 token 无法辨别
    /// </summary>
    public class EcisApiResponseHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public EcisApiResponseHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var principal = GetPrincipalAccessor();
            if (principal != null)
            {
                var currentPrincipalAccessor = Context.RequestServices.GetService<ICurrentPrincipalAccessor>();
                if (currentPrincipalAccessor == null)
                {
                    throw new InvalidOperationException("ICurrentPrincipalAccessor 未注册, 请检查 Service 注入.");
                }
                currentPrincipalAccessor.Change(principal.Claims);
                Context.User = principal;

                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("校验身份失败 (×_×)"));
        }

        private ClaimsPrincipal GetPrincipalAccessor()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return null;
            }

            try
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                // Create ClaimsPrincipal from the parsed token
                var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "Bearer");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return claimsPrincipal;
            }
            catch (Exception)
            {
                // Token validation failed
            }

            return null;
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json;charset=utf-8";
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            var ret = new ResponseResult<string>
            {
                Code = HttpStatusCodeEnum.UnAuthorized,
                Msg = "没有权限",
                Data = "Unauthorized"
            };
            return Response.WriteAsync(JsonConvert.SerializeObject(ret));
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json;charset=utf-8";
            Response.StatusCode = StatusCodes.Status403Forbidden;
            var ret = new ResponseResult<string>
            {
                Code = HttpStatusCodeEnum.Forbidden,
                Msg = "没有权限访问此资源",
                Data = "Fobidden"
            };
            return Response.WriteAsync(JsonConvert.SerializeObject(ret));
        }
    }
}