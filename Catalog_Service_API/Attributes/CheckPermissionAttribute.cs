using ConstantsLib.Enums;

namespace Catalog_Service_API.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]

    public class CheckPermissionAttribute : Attribute
    {
        public EnPermission Permission { get; }

        public CheckPermissionAttribute(EnPermission permission)
        {
            Permission = permission;
        }

    }

}
