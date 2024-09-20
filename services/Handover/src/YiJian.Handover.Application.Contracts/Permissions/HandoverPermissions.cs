using Volo.Abp.Reflection;

namespace YiJian.Handover.Permissions
{
    public class HandoverPermissions
    {
        public const string GroupName = "Handover";

        public static class NurseHandovers
        {
            public const string Default = GroupName + ".NurseHandover";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class NursePatients
        {
            public const string Default = GroupName + ".NursePatients";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(HandoverPermissions));
        }
    }
}
