using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using Vovinam.Common;
using System.Data.Entity;
using Vovinam.Common.Enums;

namespace Vovinam.Services
{
    public class TuitionService
    {
        public static PagedSearchList<Student> Search(string name, int ClubId, Tuition? HasColected, Tuition? UnColected,
     int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                IQueryable<Student> query = context.Students.Where(x => x.CompanyId == user.CompanyId)
                    .Where(x => x.ClubId == ClubId)
                    .Where(x => x.Deleted == false).AsNoTracking();

                if (name.NotEmpty())
                {
                    query = query.Where(x => x.Name.Contains(name));
                }

                if (HasColected.HasValue)
                {
                    query = SearchTuitionMonth(query, HasColected, true);
                }
                if (UnColected.HasValue)
                {
                    query = SearchTuitionMonth(query, UnColected, false);
                }

                query = query.Include(x => x.Level).Include(x => x.School).OrderByDescending(x => x.Id);

                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Student>(query, pageIndex, pageSize);
            }
        }

        private static IQueryable<Student> SearchTuitionMonth(IQueryable<Student> query, Tuition? Colected, bool IsColected)
        {
            if (Colected == Tuition.Tuition1)
                return query.Where(x => x.Tuition1 == IsColected);
            else if (Colected == Tuition.Tuition2)
                return query.Where(x => x.Tuition2 == IsColected);
            else if (Colected == Tuition.Tuition3)
                return query.Where(x => x.Tuition3 == IsColected);
            else if (Colected == Tuition.Tuition4)
                return query.Where(x => x.Tuition4 == IsColected);
            else if (Colected == Tuition.Tuition5)
                return query.Where(x => x.Tuition5 == IsColected);
            else if (Colected == Tuition.Tuition6)
                return query.Where(x => x.Tuition6 == IsColected);
            else if (Colected == Tuition.Tuition7)
                return query.Where(x => x.Tuition7 == IsColected);
            else if (Colected == Tuition.Tuition8)
                return query.Where(x => x.Tuition8 == IsColected);
            else if (Colected == Tuition.Tuition9)
                return query.Where(x => x.Tuition9 == IsColected);
            else if (Colected == Tuition.Tuition10)
                return query.Where(x => x.Tuition10 == IsColected);
            else if (Colected == Tuition.Tuition11)
                return query.Where(x => x.Tuition11 == IsColected);
            else
                return query.Where(x => x.Tuition12 == IsColected);

        }
    }
}
