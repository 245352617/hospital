using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 映射扩展方法
    /// </summary>
    public static class AutoMaperProS
    {
        /// <summary>
        ///  类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体</returns>
        public static TDestination SMapTo<TSource, TDestination>(this TSource source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return default(TDestination);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }


        /// <summary>
        /// 集合列表类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体列表</returns>
        public static List<TDestination> SMapToList<TSource, TDestination>(this IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return new List<TDestination>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 类型映射,可指定映射字段的配置信息
        /// </summary>
        /// <typeparam name="TSource">源数据：要被转化的实体对象</typeparam>
        /// <typeparam name="TDestination">目标数据：转换后的实体对象</typeparam>
        /// <param name="source">任何引用类型对象</param>
        /// <param name="cfgExp">可为null，则自动一一映射</param>
        /// <returns></returns>
        public static TDestination AutoMapTo<TSource, TDestination>(this TSource source, Action<AutoMapper.IMapperConfigurationExpression> cfgExp)
         where TDestination : class
         where TSource : class
        {
            if (source == null) return default(TDestination);
            var config = new AutoMapper.MapperConfiguration(cfgExp != null ? cfgExp : cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }
    }
}
