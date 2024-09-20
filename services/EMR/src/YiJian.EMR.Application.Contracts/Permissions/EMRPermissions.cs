using Volo.Abp.Reflection;

namespace YiJian.EMR.Permissions
{
    public class EMRPermissions
    {
        public const string GroupName = "EMR";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EMRPermissions));
        }

        public static class Catalogues
        {
            public const string Default = GroupName + ".Catalogues";
            public const string List = Default + ".List";
            public const string Detail = Default + ".Detail";
            public const string Modify = Default + ".Modify";
            public const string Delete = Default + ".Delete";

        }

        public static class TemplateCatalogues
        {
            public const string Default = GroupName + ".TemplateCatalogues";
            public const string List = Default + ".List";
            public const string Detail = Default + ".Detail";
            public const string Modify = Default + ".Modify";
            public const string Delete = Default + ".Delete";
        }

        public static class Writes
        {
            public const string Default = GroupName + ".Writes";
            public const string List = Default + ".List";
            public const string Detail = Default + ".Detail";
            public const string Modify = Default + ".Modify";
            public const string Delete = Default + ".Delete";
        }

    }
}