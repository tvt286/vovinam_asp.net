using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Vovinam.Data;
using Vovinam.Common;
using System;

namespace Vovinam.Services
{
    public class GroupPermissionService
    {
        public static List<Group> GetAll(int companyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Groups.Where(x => x.IsDeleted == false).Where(x => x.CompanyId == companyId).AsNoTracking().ToList();
            }
        }

        public static Group Get(int groupId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Groups.Where(x => x.GroupId == groupId).FirstOrDefault();
            }
        }


        public static PagedSearchList<Group> Search(string name,
            int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                IQueryable<Group> query = context.Groups.Where(x => x.IsDeleted == false).AsNoTracking();
                if (user.IsAdminRoot == false)
                {
                    query = query.Where(x => x.CompanyId == user.CompanyId);
                }

                if (name.NotEmpty())
                {
                    query = query.Where(x => x.Name == name);
                }

                query = query.OrderByDescending(x => x.GroupId);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Group>(query, pageIndex, pageSize);
            }
        }

        public static void DeleteGroup(string groupIdStr)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var groupIdList =
                    groupIdStr.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                foreach (var groupId in groupIdList)
                {
                    var group = context.Groups.First(x => x.GroupId == groupId);
                    group.IsDeleted = true;
                }
                context.SaveChanges();
            }
        }
    }
}
