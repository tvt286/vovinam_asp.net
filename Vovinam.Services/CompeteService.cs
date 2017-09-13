using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using System.Data.Entity;

namespace Vovinam.Services
{
    public class CompeteService
    {
        public static void Create(List<Compete> data)
        {
            using (var context = new vovinamEntities())
            {
                var user = UserService.GetUserInfo();

                foreach (var item in data)
                {
                    item.CompanyId = user.CompanyId;
                    context.Competes.Add(item);
                }
                context.SaveChanges();
            }
        }

        public static List<Compete> Gets(int? CompanyId, int ExaminationId, int Gender)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Competes.Where(x => x.CompanyId == CompanyId)
                    .Where(x => x.ExaminationId == ExaminationId)
                    .Where(x => x.Gender == (Gender)Gender)
                    .Include(x => x.LevelUp1)
                    .Include(x => x.LevelUp1.Club)
                    .Include(x => x.LevelUp1.Level)
                    .Include(x => x.LevelUp)
                    .Include(x => x.LevelUp.Club)
                    .Include(x => x.LevelUp.Level)
                    .ToList();
            }
        }
        public static PagedSearchList<Compete> Search(int? ExaminationId, int GenderId,
     int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                IQueryable<Compete> query = context.Competes.Where(x => x.CompanyId == user.CompanyId).Where(x => x.Gender == (Gender)GenderId).AsNoTracking();


                if (ExaminationId.HasValue)
                {
                    query = query.Where(x => x.ExaminationId == ExaminationId);
                }
                else
                {
                    query = query.Where(x => x.ExaminationId == ExaminationService.GetLastId().Id);
                }

                query = query.Include(x => x.LevelUp)
                    .Include(x => x.LevelUp.DoiKhang)
                    .Include(x => x.LevelUp.DoiKhang.User)
                    .Include(x => x.LevelUp.Club)
                    .Include(x => x.LevelUp1)
                    .Include(x => x.LevelUp1.DoiKhang)
                    .Include(x => x.LevelUp1.DoiKhang.User)
                    .Include(x => x.LevelUp1.Club)
                    .OrderBy(x => x.Id);

                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Compete>(query, pageIndex, pageSize);
            }
        }
    }
}
