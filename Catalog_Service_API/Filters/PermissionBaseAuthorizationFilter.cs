using Catalog_Service_API.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Catalog_Service_Core.Entities;
using Microsoft.AspNetCore.Identity;
using Catalog_Service_API.Extensions;

namespace Catalog_Service_API.Filters
{
    public class PermissionBaseAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
               .OfType<AllowAnonymousAttribute>()
               .Any();

            if (allowAnonymous) return;

            var attribute = context.ActionDescriptor.EndpointMetadata
                .OfType<CheckPermissionAttribute>()
                .FirstOrDefault();

            if (attribute == null) return;

            long RequiredPermission = Convert.ToInt64(attribute.Permission);
            long userPermissions = context.HttpContext.User.GetPermissions();

            if ((userPermissions & RequiredPermission) == 0)
            {
                context.Result = new StatusCodeResult(403);

            }
        }
    }

}
