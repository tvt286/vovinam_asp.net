using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vovinam.Common;
using Vovinam.Data;

namespace Vovinam.Services
{
    public class AuthorizeService
    {
        public static bool HasPermission(int permission)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            var username = HttpContext.Current.User.Identity.Name;
            string key = string.Format("AuthorizeService-Permission-{0}", username);
            var list = new MemoryCacheManager().Get(key, GetPermissionByUser);
            return list.Contains(permission);
        }

        public static bool HasPermission(Permission permission)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            var username = HttpContext.Current.User.Identity.Name;
            string key = string.Format("AuthorizeService-Permission-{0}", username);
            var list = new MemoryCacheManager().Get(key, GetPermissionByUser);
            return list.Contains((int)permission);
        }

        public static bool HasPermission(int[] permissions)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            var username = HttpContext.Current.User.Identity.Name;
            string key =string.Format("AuthorizeService-Permission-{0}", username);
            var list = new MemoryCacheManager().Get(key, GetPermissionByUser);
            return list.Any(permissions.Contains);
        }

        public static void ClearCache(string username)
        {
            string key = string.Format("AuthorizeService-Permission-{0}", username);
            new MemoryCacheManager().Remove(key);

            string key2 = string.Format("UserService-Info-{0}", username);
            new MemoryCacheManager().Remove(key2);
        }

        public static List<int> GetPermissionByUser()
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var username = HttpContext.Current.User.Identity.Name;
                var user =
                    context.Users.Include(x => x.User_Permission).Include(x => x.Groups.Select(y => y.Group_Permission))
                        .FirstOrDefault(x => x.UserName == username);
                if (user == null)
                {
                    return new List<int>();
                }
                else
                {
                    var result = user.Groups.SelectMany(x => x.Group_Permission.Select(y => y.PermissionId)).ToList().Union(user.User_Permission.Select(x => x.PermissionId));
                    return result.Distinct().ToList();
                }
            }
        }

        
    }
}
