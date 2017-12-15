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
    public class ExaminationService
    {
        public static PagedSearchList<Examination> Search(int? ExaminationId,
          int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                IQueryable<Examination> query = context.Examinations.Where(x => x.CompanyId == user.CompanyId && x.IsDeleted == false).AsNoTracking();

                if (ExaminationId.HasValue)
                {
                    query = query.Where(x => x.Id == ExaminationId);
                }

                query = query.OrderByDescending(x => x.Id);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;

                return new PagedSearchList<Examination>(query, pageIndex, pageSize);
            }
        }

        public static void Delete(string schoolIdStr)
        {
            using (var context = new vovinamEntities())
            {
                var schoolIdList =
                    schoolIdStr.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                foreach (var schoolId in schoolIdList)
                {
                    var school = context.Examinations.FirstOrDefault(x => x.Id == schoolId);
                    school.IsDeleted = true;
                    context.SaveChanges();
                }
            }
        }
        public static List<Examination> GetAll(int companyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Examinations.Where(x => x.CompanyId == companyId && x.IsDeleted == false).AsNoTracking().ToList();
            }
        }

        public static Examination GetByName(string name)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Examinations.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Name == name);
            }
        }

        public static void Create(Examination data)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                data.Date = DateTime.Now;
                data.CompanyId = user.CompanyId;
                context.Examinations.Add(data);
                context.SaveChanges();
            }
        }

        public static Examination GetLastId()
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Examinations.Where(x => x.IsDeleted == false).OrderBy(x => x.Id).ToList().LastOrDefault();
            }
        }
    }
}
