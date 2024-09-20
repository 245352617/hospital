using System.Linq;
using System.Reflection;
using Serilog;

namespace YiJian.BodyParts.Domain.Shared.Cache
{
    public class GenericCache<T> where T:class,new()
    {
        private static readonly PropertyInfo[] _props;
        private static readonly string[] _propNames;
        static GenericCache()
        {
            var type = typeof(T);
            _props = type.GetProperties();
            _propNames = _props.Select(s => s.Name.ToUpper()).ToArray();
        }

        public static object GetValue(T tModel, string propName)
        {
            if (!_propNames.Contains(propName.ToUpper()))
            {
                Log.Warning($"[{nameof(T)}]不包含[{propName}]属性");
                return null;
            }

            var currPropInfo = _props.FirstOrDefault(f => f.Name.ToUpper() == propName.ToUpper());
            return currPropInfo.GetValue(tModel);
        }
    }
}
