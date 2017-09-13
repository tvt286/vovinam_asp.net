using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using Vovinam.Common;

namespace Vovinam.Services
{
    public class ClubService
    {
       

        public static Club Get(int id)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Clubs.First(x => x.Id == id);
            }
        }

        public static Club GetByName(string name)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Clubs.First(x => x.Name.Contains(name));
            }
        }

        public static void CreateClub(Club data)
        {
            using (var context = new vovinamEntities())
            {
                var user = UserService.GetUserInfo();
                data.CompanyId = (int)user.CompanyId;
                data.DateCreated = DateTime.Now;
                context.Clubs.Add(data);
                context.SaveChanges();
            }
        }

        public static void UpdateClub(Club data)
        {
            using (var context = new vovinamEntities())
            {
                context.Clubs.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.Entry(data).Property(x => x.DateCreated).IsModified = false;
                context.SaveChanges();
            }
        }

        public static PagedSearchList<Club> Search(string ClubName,
            int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                IQueryable<Club> query = context.Clubs.Where(x => x.IsDeleted == false).Where(x => x.CompanyId == user.CompanyId).AsNoTracking();

                if (ClubName.NotEmpty())
                {
                    query = query.Where(x => x.Name.Contains(ClubName));
                }

                query = query.OrderByDescending(x => x.Id);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Club>(query, pageIndex, pageSize);
            }
        }

        public static void DeleteClub(string clubIdStr)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var clubIdList =
                    clubIdStr.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                foreach (var clubId in clubIdList)
                {
                    var club = context.Clubs.First(x => x.Id == clubId);
                    club.IsDeleted = true;
                }
                context.SaveChanges();
            }
        }
    }
}
