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
        public static List<Examination> GetAll(int companyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Examinations.Where(x => x.CompanyId == companyId).AsNoTracking().ToList();
            }
        }

        public static Examination GetByName(string name)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Examinations.FirstOrDefault(x => x.Name == name);
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
                return context.Examinations.OrderBy(x => x.Id).ToList().LastOrDefault();
            }
        }
    }
}
