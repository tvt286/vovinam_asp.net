using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;

namespace Vovinam.Services
{
    public class LevelService
    {
        public static List<Level> GetByCompanyId(int CompanyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Levels.Where(x => x.CompanyId == CompanyId).ToList();
            }
        }
        public static Level GetByName(string name)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Levels.FirstOrDefault(x => x.Name == name);
            }
        }
    }
}
