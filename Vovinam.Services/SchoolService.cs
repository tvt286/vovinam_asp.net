using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using Vovinam.Common;
using System.Data.Entity;

namespace Vovinam.Services
{
    public class SchoolService
    {
        public static List<School> GetByCompanyId(int CompanyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {             
                return context.Schools.Where(x => x.CompanyId == CompanyId).ToList();
            }
        }

        public static School Get(int id)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Schools.FirstOrDefault(x => x.Id == id);
            }
        }

        public static School GetByName(string name)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Schools.First(x => x.Name.Contains(name));
            }
        }

        public static void Create(School data)
        {
            using (var context = new vovinamEntities())
            {
                var user = UserService.GetUserInfo();
                data.CompanyId = user.CompanyId;
                context.Schools.Add(data);
                context.SaveChanges();
            }
        }

        public static void Update(School data)
        {
            using (var context = new vovinamEntities())
            {
                context.Schools.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static PagedSearchList<School> Search(string SchoolName,
            int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                IQueryable<School> query = context.Schools.Where(x => x.CompanyId == user.CompanyId).AsNoTracking();

                if (SchoolName.NotEmpty())
                {
                    query = query.Where(x => x.Name.Contains(SchoolName));
                }

                query = query.OrderByDescending(x => x.Id);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<School>(query, pageIndex, pageSize);
            }
        }

        public static void Delete(string schoolIdStr)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var schoolIdList =
                    schoolIdStr.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                foreach (var schoolId in schoolIdList)
                {
                    var school = context.Schools.FirstOrDefault(x => x.Id == schoolId);
                    if(school != null)
                    context.Schools.Remove(school);
                }
                context.SaveChanges();
            }
        }
    }
}
