using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace YiJian.BodyParts
{
    public static class StringExten
    {
        /// <summary>
        /// 字符串为空?
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmptyOrNull(this string str)
        {
            return str == null || str.Length <= 0;
        }

        /// <summary>
        /// 字符串不为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubstringByLength(this string str, int length)
        {
            if (str == null || length <= 0 || str.Length <= length) return str;

            return str.Substring(0, length);
        }

        /// <summary>
        /// 是否为手机号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobilPhone(this string str, bool isThrowExcetion = false)
        {
            Regex RegMobilePhone = new Regex(@"^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$"); //手机号码
            Match m = RegMobilePhone.Match(str);
            if (!m.Success && isThrowExcetion)
            {
                throw new Exception($"当前手机号【{str}】格式出错，请检查");
            }

            return m.Success;
        }

        public static ReturnResult<T> ToReturnResult<T>(this T m)
        {
            return ReturnResult<T>.Ok(data: m);
        }

        /// <summary>
        /// 对象复制，只复制不为空的数据
        /// </summary>
        /// <param name="target">目标对像</param>
        /// <param name="source"></param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="T1">源类型</typeparam>
        /// <returns></returns>
        public static T CopyToT<T, T1>(this T target, T1 source) where T : class where T1 : class
        {
            var props = target.GetType().GetProperties();

            var props1 = source.GetType().GetProperties();

            foreach (var p in props1)
            {
                var v = p.GetValue(source, null);
                if (v == null || (v is string s && s == "")) continue;

                var p2 = props.FirstOrDefault(a => a.Name == p.Name);
                if (p2 == null) continue;

                if (p2.CanWrite)
                    p2.SetValue(target, v);
            }

            return target;
        }

        /// <summary>
        /// 复制除ID外的其他数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T CopyToWithoutId<T, T1>(this T target, T1 source) where T : class where T1 : class
        {
            var props = target.GetType().GetProperties();

            var props1 = source.GetType().GetProperties();

            foreach (var p in props1)
            {
                if (p.Name.ToUpper() == "ID") continue;

                var v = p.GetValue(source, null);

                var p2 = props.FirstOrDefault(a => a.Name == p.Name);
                if(p2 == null) continue;
                if (p2.CanWrite)
                    p2.SetValue(target, v);
            }

            return target;
        }

        /// <summary>
        /// 对象复制
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fromDto"></param>
        /// <param name="KeyName"></param>
        /// <param name="notUpdateField"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public static TEntity CopyFromDto<TEntity, TDto>(TEntity data, TDto fromDto, string KeyName = null,
            List<string> notUpdateField = null) where TEntity : class
            where TDto : class
        {
            var atype = data.GetType();

            var allpros = atype.GetProperties();

            var allpros2 = fromDto.GetType().GetProperties();

            if (notUpdateField.IsEmptyOrNull())
                notUpdateField = new List<string> { "CreationTime", "LastModificationTime" };

            foreach (var row in allpros2)
            {
                if ((KeyName.IsNotEmptyOrNull() && row.Name == KeyName) ||
                    (notUpdateField.IsNotEmptyOrNull() && notUpdateField.Contains(row.Name))) continue;

                var idval = row.GetValue(fromDto, null);
                if (idval != null)
                {
                    var idpro = allpros.FirstOrDefault(a => a.Name == row.Name);
                    if (idpro != null && idpro.CanWrite)
                    {
                        idpro.SetValue(data, idval);
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// 将string的Null设置为空的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RemoveNullField<T>(this T data) => _RemoveNullField(data);

        static dynamic _RemoveNullField(dynamic data)
        {
            if (data == null) return null;

            Type t = data.GetType();

            var isGenericTypeList = false;

            if (t.IsGenericType)
            {
                int count = Convert.ToInt32(t.GetProperty("Count")?.GetValue(data, null));
                if (count > 0)
                {
                    isGenericTypeList = true;
                    for (var i = 0; i < count; i++)
                    {
                        object item = t.GetProperty("Item")?.GetValue(data, new object[] { i });
                        item.RemoveNullField();
                    }
                }
            }

            if (!isGenericTypeList && !t.IsValueType)
            {
                var pops = t.GetProperties();

                foreach (PropertyInfo p in pops)
                {
                    var propertyType = p.PropertyType;
                    if (!(p.CanRead && p.CanWrite)) continue;

                    if (propertyType != typeof(string) && propertyType != typeof(String)) continue;

                    var v = p.GetValue(data, null);

                    if (v == null) p.SetValue(data, "");
                }
            }

            return data;
        }

        /// <summary>
        /// 将Null设置为空的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static dynamic RemoveNullField(dynamic data) => _RemoveNullField(data);

        /// <summary>
        /// 双方模型都有值的情况下，合并更新
        /// </summary>
        /// <typeparam name="TEntity">被修改的模型</typeparam>
        /// <typeparam name="TDto">部分更改的模型</typeparam>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <returns></returns>
        public static TEntity NotNullModel<TDto, TEntity>(this TDto entity1, TEntity entity2)
        {
            //通过反射获取属性
            var dic = entity1.GetType().GetProperties();

            //用dic
            Dictionary<string, dynamic> dicinsp = new Dictionary<string, dynamic>();

            foreach (var item in dic)
            {
                //获取值
                dynamic values = item.GetValue(entity1, null);

                if (values != null)
                    //不为空,存进字典
                    dicinsp.Add(item.Name, values);
            }

            //通过反射找出列名，赋值进去
            foreach (var item in dicinsp)
            {
                entity2.GetType().GetProperty(item.Key)?.SetValue(entity2, item.Value, null);
            }

            //返回原有模型
            return entity2;
        }
    }
}