using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Vovinam.Data;
using Vovinam.Common;

namespace Vovinam.Services
{
    public class StudentService
    {
        public static PagedSearchList<Student> Search(string name, int? levelId, int? clubId,
       int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                IQueryable<Student> query = context.Students.Where(x => x.CompanyId == user.CompanyId).Where(x => x.CompanyId == user.CompanyId).Where(x => x.ClubId == clubId).Where(x => x.Deleted == false).AsNoTracking();

                if (name.NotEmpty())
                {
                    query = query.Where(x => x.Name.Contains(name));
                }

                if (levelId.HasValue)
                {
                    query = query.Where(x => x.LevelId == levelId.Value);
                }

                query = query.Include(x => x.Level).Include(x => x.School).OrderByDescending(x => x.Id);

                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<Student>(query, pageIndex, pageSize);
            }
        }

        public static Student Get(int id, bool includeDetail = false)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var query = context.Students.Where(x => x.Id == id);
                if (includeDetail)
                {
                    query = query.Include(x => x.Level).Include(x => x.School).Include(x => x.Club);
                }
                return query.First();
            }
        }

        public static void Create(Student data)
        {
            using (var context = new vovinamEntities())
            {
                data.DateCreated = DateTime.Now;
                if (data.LevelId > 6)
                    data = setTuition(data, true);
                else
                    data = setTuition(data, false);

                context.Students.Add(data);
                context.SaveChanges();
            }
        }

        private static Student setTuition(Student data, bool collected)
        {
            data.Tuition1 = collected;
            data.Tuition2 = collected;
            data.Tuition3 = collected;
            data.Tuition4 = collected;
            data.Tuition5 = collected;
            data.Tuition6 = collected;
            data.Tuition7 = collected;
            data.Tuition8 = collected;
            data.Tuition9 = collected;
            data.Tuition10 = collected;
            data.Tuition11 = collected;
            data.Tuition12 = collected;

            return data;
        }

        public static void Update(Student data)
        {
            using (var context = new vovinamEntities())
            {

                data.DateUpdated = DateTime.Now;
                context.Students.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.Entry(data).Property(x => x.DateCreated).IsModified = false;
                context.SaveChanges();
            }
        }

        public static void DeleteStudent(string studentIdStr)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var studentIdList =
                    studentIdStr.Replace(" ", "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                foreach (var studentId in studentIdList)
                {
                    var student = context.Students.First(x => x.Id == studentId);
                    student.Deleted = true;
                    student.TimeDeleted = DateTime.Now;
                }
                context.SaveChanges();
            }
        }
    }
}
