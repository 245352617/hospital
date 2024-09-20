using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace YiJian.Job.Extensions;

public static class CustomServiceExtensions
{

    /// <summary>
    /// 注册自定义的后台服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var batchServices = GetConfigureClass("YiJian.Job");
          
        foreach (var type in batchServices)
        {
            type.Value.ToList().ForEach(x => { 
                services.AddScoped(x,type.Key);
            });
        } 
        return services;
    }


    /// <summary>
    /// 根据程序集名称获取自定义服务
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static Dictionary<Type, Type[]> GetConfigureClass(string assemblyName)
    {
        Dictionary<Type, Type[]> dic = new Dictionary<Type, Type[]>();
        if (!string.IsNullOrEmpty(assemblyName))
        {
            //获取程序集对应的类型 
            Assembly assembly = Assembly.Load(assemblyName); //Assembly.LoadFrom(assembly);

            List<Type> lstType = assembly.GetTypes().ToList();

            lstType.ForEach(x =>
            {
                if (x.Namespace != null && x.Namespace.Contains("YiJian.Job.BackgroundService"))
                {
                    //筛选满足条件的服务类
                    if (x.IsClass && x.GetInterfaces().Length > 0)
                    {
                        dic.Add(x, x.GetInterfaces());
                    }
                }
            });
        }
        return dic;
    }

}
