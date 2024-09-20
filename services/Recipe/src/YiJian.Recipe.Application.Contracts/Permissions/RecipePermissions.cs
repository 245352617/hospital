using Volo.Abp.Reflection;

namespace YiJian.Recipe.Permissions
{
    public class RecipePermissions
    {
        public const string GroupName = "Recipe";

        public static class OperationApplies
        {
            public const string Default = GroupName + ".OperationApply";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Recipe
        {
            public const string LookUp = GroupName + ".LookUp";
            public const string Create = GroupName + ".Create";
            public const string Submit = GroupName + ".Submit";
            public const string Copy = GroupName + ".Copy";
            public const string Stop = GroupName + ".Stop";
            public const string Save = GroupName + ".Save";
            public const string Cancel = GroupName + ".Cancel";
            public const string Delete = GroupName + ".Delete";
        }

        public static class Lis
        {
            public const string Default = GroupName + ".Lis";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Pacs
        {
            public const string Default = GroupName + ".Pacs";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Treats
        {
            public const string Default = GroupName + ".Treat";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Prescribes
        {
            public const string Default = GroupName + ".Prescribe";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class InviteDoctors
        {
            public const string Default = GroupName + ".InviteDoctor";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class GroupConsultations
        {
            public const string Default = GroupName + ".GroupConsultation";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(RecipePermissions));
        }
    }
}
