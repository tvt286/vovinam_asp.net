using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Vovinam.Services;
using Vovinam.Common;

namespace Vovinam.WebBackend.Common
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {

        public Permission[] Permissions { get; set; }

        public Permission Permission;


        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // call base method            
            var result = base.AuthorizeCore(httpContext);
            // if base return true, check additional logic base on Permissions
            var user = UserService.GetUserInfo();
            if (user != null && user.IsAdminCompany)
                return true;
            if (result)
            {
                return AuthorizeService.HasPermission((int) Permission) ||
                       (Permissions != null && AuthorizeService.HasPermission(Permissions.Select(x => (int) x).ToArray()));
            }
            return false;
        }
    }

    public class UserPermission
    {
        public static bool Has(Permission p)
        {
            var user = UserService.GetUserInfo();
            if (user.IsAdminCompany)
                return true;
            return AuthorizeService.HasPermission((int)p);
        }
    }
}