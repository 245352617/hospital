using Volo.Abp.Reflection;

namespace YiJian.ECIS.IMService.Permissions
{
    public class IMServicePermissions
    {
        public const string GroupName = "IMService";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IMServicePermissions));
        }
    }
}