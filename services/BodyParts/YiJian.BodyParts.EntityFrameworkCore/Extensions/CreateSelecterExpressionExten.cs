using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YiJian.BodyParts
{
    public static class CreateSelecterExpressionExten
    {
        public static Expression<Func<TSource, TResult>> CreateSelecter<TSource, TResult>()
        {
            Expression<Func<TSource, TResult>> selector = null;
            var ts = typeof(TSource);//源
            var tr = typeof(TResult);//目标
            
            ParameterExpression param = Expression.Parameter(ts, "x");
            var v0 = Expression.New(tr);
            List<MemberBinding> bindingList = new List<MemberBinding>();
            
            var taproot = ts.GetProperties();
            var taproomsNames = taproot.Select(a=>a.Name);

            var taproomsAll = tr.GetProperties();
            var taprooms = taproomsAll.Select(a=>a.Name).Reverse().ToList();
            var stype = typeof(String);
            
            foreach (var Name in taprooms)
            {
                if (!taproomsNames.Contains(Name)) continue;
                
                var p = taproomsAll.FirstOrDefault(a => a.Name == Name);
                
                var pro = p.PropertyType;
                
                if ((!pro.IsValueType && pro != stype) && Name != "ExtraProperties") continue;

                if (Name == "ExtraProperties" && pro != taproot.FirstOrDefault(a => a.Name == Name)?.PropertyType)
                {
                    Console.WriteLine($"dto.{Name}字段与model.{Name}字段类型不一致，不读取", Color.Red);
                    continue;
                }
                
                var v = Expression.Convert(GetProperty<TSource>(null, Name, param), pro);
                
                var m = Expression.Bind(p, v);
                
                bindingList.Add(m);
            }
            
            Expression body = Expression.MemberInit(v0, bindingList);

            selector = (Expression<Func<TSource, TResult>>)Expression.Lambda(body, param);

            return selector;
        }
        
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName,string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
        
        private static Expression<Func<TSource, TResult>> CreateSelecter<TSource, TResult>(Dictionary<string,string> fieldDic)
        {
            Expression<Func<TSource, TResult>> selector = null;
            
            //(rec)
            ParameterExpression param = Expression.Parameter(typeof(TSource), "x");
            //new ParadigmSearchListData 
            var v0 = Expression.New(typeof(TResult));
            //Number
            List<MemberBinding> bindingList = new List<MemberBinding>();
            foreach (var item in fieldDic)
            {
                var p = typeof(TResult).GetProperty(item.Key);
                Expression right = GetProperty<TSource>(null, item.Value, param);
                //right= Expression.Constant(right, p.PropertyType);
                var v = Expression.Convert(GetProperty<TSource>(null, item.Value, param), p.PropertyType);
                var m = Expression.Bind(p, v);
                bindingList.Add(m);
            }
            Expression body = Expression.MemberInit(v0, bindingList);


          selector = (Expression<Func<TSource, TResult>>)Expression.Lambda(body, param);


            return selector;
        }


        public static Expression GetProperty<T>(Expression source, string Name, ParameterExpression Param)
        {
            Name = Name.Replace(")", "");
            string[] propertys = null;
            if (Name.Contains("=>"))
            {
                propertys = Name.Split('.').Skip(1).ToArray();
            }
            else
            {
                propertys = Name.Split('.');
            }
            if (source == null)
            {
                source = Expression.Property(Param, typeof(T).GetProperty(propertys.First()));
            }
            else
            {
                source = Expression.Property(source, propertys.First());
            }
            foreach (var item in propertys.Skip(1))
            {
                source = GetProperty<T>(source, item, Param);
            }
            return source;
        } 
    }
}