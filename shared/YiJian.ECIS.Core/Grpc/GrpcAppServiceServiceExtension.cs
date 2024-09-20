using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace YiJian.ECIS.Grpc;

/// <summary>
/// gRPC 扩展方法
/// </summary>
public static class GrpcAppServiceServiceExtension
{
    public static IServiceCollection AddAppServiceGrpc(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        return AddAppServiceGrpc(services, assembly);
    }


    public static IServiceCollection AddAppServiceGrpc(this IServiceCollection services, Assembly assembly)
    {
        services.AddGrpc();
        services.Configure<AbpEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                var grpcAppServices = assembly.GetTypes().Where(t => t.GetInterface(nameof(IGrpcAppService)) != null);
                foreach (var grpcAppService in grpcAppServices)
                {
                    var methodInfo = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService", BindingFlags.Public | BindingFlags.Static);
                    var genericMethod = methodInfo.MakeGenericMethod(grpcAppService)
                        .Invoke(services, new object[] { endpointContext.Endpoints });
                }
                //endpointContext.Endpoints.MapGet("/protos/masterdata.proto", async context =>
                //{
                //    await context.Response.WriteAsync(File.ReadAllText("Protos/masterdata.proto"));
                //});
            });
        });

        return services;
    }
}
