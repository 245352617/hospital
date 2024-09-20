using Volo.Abp.Reflection;

namespace YiJian.MasterData.Permissions;

public class MasterDataPermissions
{
    public const string GroupName = "MasterData";

    public static class Sequences
    {
        public const string Default = GroupName + ".Sequence";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class LabContainers
    {
        public const string Default = GroupName + ".LabContainer";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class LabSpecimenPositions
    {
        public const string Default = GroupName + ".LabSpecimenPosition";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class Categories
    {
        public const string Default = GroupName + ".Category";
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

    public static class ViewSettings
    {
        public const string Default = GroupName + ".ViewSetting";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class MedicineUsages
    {
        public const string Default = GroupName + ".MedicineUsage";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class AllItems
    {
        public const string Default = GroupName + ".AllItem";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class VitalSignExpressions
    {
        public const string Default = GroupName + ".VitalSignExpression";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ExamProjects
    {
        public const string Default = GroupName + ".ExamProject";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ExamCatalogs
    {
        public const string Default = GroupName + ".ExamCatalog";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ExamParts
    {
        public const string Default = GroupName + ".ExamPart";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ExamTargets
    {
        public const string Default = GroupName + ".ExamTarget";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ExamNotes
    {
        public const string Default = GroupName + ".ExamNote";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class LabCatalogs
    {
        public const string Default = GroupName + ".LabCatalog";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class LabProjects
    {
        public const string Default = GroupName + ".LabProject";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class LabTargets
    {
        public const string Default = GroupName + ".LabTarget";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class LabSpecimens
    {
        public const string Default = GroupName + ".LabSpecimen";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MasterDataPermissions));
    }
}
