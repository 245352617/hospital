namespace YiJian.BodyParts
{
    public static class JsonExten
    {
        /// <summary>
        /// 对象列表化
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToJson<T>(this T model) where T : class
        {
            if (model != null)
            {
                return JsonHelper.SerializeObject(model);
            }

            return null;
        }
    }
}